using System;
using RabbitMQ.Client;
using CustomerRegistration.Domain.Messaging;

namespace CustomerRegistration.Infrastructure.Messaging
{
    public class RabbitMqSetupService : IMessageBusSetupServer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string _exchange = "customer-service-exchange";
        private const string _queueMainAddressRegistered = "customer_main_address_registered_queue";
        private const string _queueMainAddressRegisteredDeadLetter = "customer_main_address_registered_dead_letter_queue";

        public RabbitMqSetupService(IConnection connection, IModel channel)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _channel = channel ?? throw new ArgumentNullException(nameof(channel));
        }

        public void Setup()
        {
            SetupExchange();
            Console.WriteLine("Setup RabbitMQ: Exchanges Ok.");
            SetupQueues();
            Console.WriteLine("Setup RabbitMQ: Queues Ok.");
        }

        private void SetupExchange()
        {
            // Declara a exchange principal
            _channel.ExchangeDeclare(_exchange, ExchangeType.Direct, durable: true);
        }

        private void SetupQueues()
        {
            // Configurações da dead-letter queue
            var args = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", _exchange }, // Opcionalmente, você pode configurar um exchange para a DLQ
                { "x-message-ttl", 60000 } // Tempo de vida da mensagem na DLQ (opcional)
            };

            // Declarar a dead-letter queue
            _channel.QueueDeclare(_queueMainAddressRegisteredDeadLetter, durable: true, exclusive: false, autoDelete: false, arguments: args);

            // Fila principal, que receberá as mensagens
            _channel.QueueDeclare(queue: _queueMainAddressRegistered,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: args);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
