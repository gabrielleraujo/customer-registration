using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace CustomerRegistration.Application.Messaging
{
    public abstract class RabbitMqConsumerBackgroundServiceTemplate : BackgroundService
    {
        protected readonly IModel _channel;
        protected readonly IConnection _connection;
        protected readonly ILogger<RabbitMqConsumerBackgroundServiceTemplate> _logger;
        protected readonly IServiceProvider _serviceProvider;
        protected bool _disposed = false;

        protected readonly string _exchange;
        protected readonly string _queue;
        protected readonly string _queueDeadLetter;

        public RabbitMqConsumerBackgroundServiceTemplate(
            IConnection connection, IModel channel, ILogger<RabbitMqConsumerBackgroundServiceTemplate> logger, IServiceProvider serviceProvider,
            string exchange, string queue, string queueDeadLetter)
        {
            _exchange = exchange;
            _queue = queue;
            _queueDeadLetter = queueDeadLetter;

            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(RabbitMqConsumerBackgroundServiceTemplate)} - START ==============================================================================================");
            _logger.LogInformation("Starting RabbitMQ consumer.");

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"Received message: {message}");

                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (exception, timeSpan, retryCount, context) =>
                        {
                            _logger.LogError($"Retry {retryCount} after {timeSpan.TotalSeconds} seconds due to {exception.Message}");
                        });

                var routingKey = ea.RoutingKey;

                try
                {
                    await policy.ExecuteAsync(async () =>
                    {
                        // Processamento da mensagem
                        _logger.LogInformation($"Processing message: {message}");
                        // Isso pode envolver lógica de negócio, por exemplo, registrar o cliente ou realizar operações de banco de dados.
                        var validationResult = await ProcessMessage(message);
                        var result = validationResult.IsValid;
                        
                        if (result == false)
                        {
                            _logger.LogInformation($"Validation result: {result}\nMessage rejected by handler, NOT processed message: {message}");
                            // Se falhar após todas as tentativas, enviar para a DLQ
                            SendToDeadLetterQueue(body, routingKey);
                            return Task.CompletedTask;
                        }

                        _logger.LogInformation($"Validation result: {result}\nMessage processed successfully: {message}");

                        // Confirmar o ACK manualmente após processar
                        _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                        return Task.CompletedTask;
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to process message: {ex.Message}. Sending to dead-letter queue.");

                    // Se falhar após todas as tentativas, enviar para a DLQ
                    SendToDeadLetterQueue(body, routingKey);
                }
            };

            _channel.BasicConsume(queue: _queue, autoAck: false, consumer: consumer);
            _logger.LogInformation($"{nameof(RabbitMqConsumerBackgroundServiceTemplate)} - END ==============================================================================================");

            return Task.CompletedTask;
        }

        /// <summary>
        /// ==== Implement your business logic here ====
        /// </summary>
        /// <param name="message"></param>
        protected abstract Task<ValidationResult> ProcessMessage(string message);

        private void SendToDeadLetterQueue(byte[] body, string routingKey)
        {
            // Publicar a mensagem na Dead-Letter Queue
            _channel.BasicPublish(
                exchange: _exchange,
                routingKey: _queueDeadLetter,
                basicProperties: null,
                body: body
            );
            _logger.LogInformation($"Message sent to dead-letter queue: {routingKey}");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Stopping RabbitMQ consumer.");
            await base.StopAsync(stoppingToken);
        }

        protected void RunDispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Liberar os recursos gerenciados
                    _channel?.Close();
                    _channel?.Dispose();
                    _connection?.Close();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
