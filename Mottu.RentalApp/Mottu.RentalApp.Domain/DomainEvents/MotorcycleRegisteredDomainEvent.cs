using Mottu.RentalApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Domain.DomainEvents
{
    public class MotorcycleRegisteredDomainEvent : BaseDomainEvent
    {
        public Guid MotorcycleId { get; }
        public int Year { get; }
        public string Model { get; }
        public string Plate { get; }

        public MotorcycleRegisteredDomainEvent(Guid motorcycleId, int year, string model, LicensePlate plate)
        {
            MotorcycleId = motorcycleId;
            Model = model;
            Plate = plate.Value;
            Year = year;
        }
    }
}
