using System;

namespace Mottu.RentalApp.Infrastructure.Persistence.Entities
{
    public class MotorcycleNotification
    {
        public Guid Id { get; set; }
        public Guid MotorcycleId { get; set; }
        public int Year { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
        public DateTime OccurredAtUtc { get; set; }
    }
}
