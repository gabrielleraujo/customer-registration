using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using System.Text;
using Newtonsoft.Json;
using Polly;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomerRegistration.Application.Messaging.Consume.Demo
{
    public class RabbitMqDemoConsumer //: RabbitMqConsumerBackgroundServiceTemplate
    {
        // // // Se aplicavel.
        // //private const string _routingKeySubscribe = "customer_main_address_registered";

        // public RabbitMqDemoConsumer(IConnection connection, IModel channel, ILogger<RabbitMqDemoConsumer> logger, IServiceProvider serviceprovider)
        //     : base(connection, channel, logger, serviceprovider,
        //           exchange: "customer-service-exchange",
        //           queue: "demo_queue",
        //           queueDeadLetter: "demo_dead_letter_queue")
        // { 
        //     // // Se aplicavel.
        //     // // Vincular a fila ao exchange com a chave de roteamento "customer_main_address_registered_event_key"
        //     // _channel.QueueBind(queue: _queue, 
        //     //                   exchange: _exchange, 
        //     //                   routingKey: _routingKeySubscribe);
        // }

        // protected override async Task<ValidationResult> ProcessMessage(string message)
        // {
        //     // Implement your business logic here
        //     throw new Exception();
        // }

        // protected virtual void Dispose(bool disposing)
        // {
        //     RunDispose(disposing);
        // }
    }
}
