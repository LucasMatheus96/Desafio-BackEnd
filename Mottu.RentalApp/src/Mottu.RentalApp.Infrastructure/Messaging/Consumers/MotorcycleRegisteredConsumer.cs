using System.Threading.Tasks;
using MassTransit;
using Mottu.RentalApp.Infrastructure.Persistence.Context;
using Mottu.RentalApp.Infrastructure.Persistence.Entities; // we'll create a MotorcycleNotification entity in infra
using System;
using Mottu.RentalApp.Domain.DomainEvents;

namespace Mottu.RentalApp.Infrastructure.Messaging.Consumers
{
    public class MotorcycleRegisteredConsumer : IConsumer<MotorcycleRegisteredDomainEvent>
    {
        private readonly RentalDbContext _db;

        public MotorcycleRegisteredConsumer(RentalDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<MotorcycleRegisteredDomainEvent> context)
        {
            var evt = context.Message;

            // If year == 2024, persist to notifications table
            if (evt.Year == 2024)
            {
                var notification = new MotorcycleNotification
                {
                    Id = Guid.NewGuid(),
                    MotorcycleId = evt.MotorcycleId,
                    Year = evt.Year,
                    Model = evt.Model,
                    Plate = evt.Plate,
                    OccurredAtUtc = DateTime.UtcNow
                };
                _db.MotorcycleNotifications.Add(notification);
                await _db.SaveChangesAsync();
            }

            // acknowledge automatically
        }
    }
}
