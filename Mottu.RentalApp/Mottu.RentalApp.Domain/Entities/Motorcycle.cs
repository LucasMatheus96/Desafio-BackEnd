using Mottu.RentalApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Domain.Entities
{
    public class Motorcycle
    {
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; private set; } = default!;
        public string Plate { get; private set; } = default!;
        public DateTime CreatedAtUtc { get; private set; }
        public bool IsRemoved { get; private set; }

        protected Motorcycle() { }

        private Motorcycle(Guid id, int year, string model, LicensePlate plate)
        {
            Id = id;
            Year = year;
            Model = model;
            Plate = plate.Value;
            CreatedAtUtc = DateTime.UtcNow;
            IsRemoved = false;
        }
        public static Motorcycle Create(Guid id, int year, string model, LicensePlate plate)
        {
            if (year <= 2000)
                throw new ArgumentException("Year is invalid", nameof(year));

            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model is required.", nameof(model));

            var moto = new Motorcycle(id, year, model.Trim(), plate);

            return moto;

        }

        public void UpdatePlate(LicensePlate plate)
        {
            Plate = plate.Value;
        }

        public void Remove()
        {
            IsRemoved = true;
        }
    }
}
