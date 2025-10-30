using MassTransit;
using Mottu.RentalApp.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Infrastructure.Messaging
{
    public class RabbitMqEventPublisher : IEventPublisher
    {
        private readonly IBus _bus;

        public RabbitMqEventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T @event, string queueName)
        {
            // MassTransit publishes to exchanges; queueName is left for routing or consumers.
            await _bus.Publish(@event);
        }
    }
}
