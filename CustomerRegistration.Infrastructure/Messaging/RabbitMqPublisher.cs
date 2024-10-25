using System.Text;
using Microsoft.Extensions.Logging;
using CustomerRegistration.Domain.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Polly;

namespace CustomerRegistration.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMessageBusPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqPublisher> _logger;
        private const string _exchange = "customer-service-exchange";
        private const string _queueMainAddressRegisteredDeadLetter = "customer_main_address_registered_dead_letter_queue";

        public RabbitMqPublisher(IConnection connection, IModel channel, ILogger<RabbitMqPublisher> logger)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void PublishMessage(object data, string routingKey)
        {
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning($"Retry {retryCount} after {timeSpan.TotalSeconds} seconds due to {exception.Message}");
                    });

            var payload = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(payload);

            _logger.LogInformation($"{nameof(RabbitMqPublisher)} - START ==============================================================================================");
            _logger.LogInformation($"Publish message: {payload}");

            try
            {
                policy.Execute(() =>
                {
                    _logger.LogInformation($"Publish message: {payload}");
                    _channel.BasicPublish(_exchange, routingKey, null, byteArray);
                    _logger.LogInformation($"Message published with routing key: {routingKey}");
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to publish message: {ex.Message}");

                // Enviar para Dead Letter Queue
                _channel.BasicPublish(_exchange, _queueMainAddressRegisteredDeadLetter, null, byteArray);
                _logger.LogInformation($"Message sent to dead-letter queue: {data.GetType().Name}");
            }
            _logger.LogInformation($"{nameof(RabbitMqPublisher)} - END ==============================================================================================");
        }
    }
}
