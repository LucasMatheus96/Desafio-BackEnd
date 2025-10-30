using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Interfaces.Services
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event, string queueName);
    }
}
