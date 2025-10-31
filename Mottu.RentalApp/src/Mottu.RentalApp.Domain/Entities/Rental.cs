using Mottu.RentalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Domain.Entities
{
    public class Rental
    {
        public Guid Id { get; private set; }
        public string RiderId { get; private set; }
        public string MotorcycleId { get; private set; }
        public DateTime StartDateUtc { get; private set; }        
        public DateTime PlannedEndDateUtc { get; private set; } 
        public DateTime? EndDateUtc { get; private set; }        
        public PlanType PlanType { get; private set; }
        public decimal DailyRate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public RentalStatus Status { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

         public Motorcycle Motorcycle { get; private set; } = default!;
    public Rider Rider { get; private set; } = default!;
        protected Rental() { }

        private Rental(Guid id, string riderId, string motorcycleId, DateTime startDateUtc, DateTime plannedEndDateUtc, PlanType planType, decimal dailyRate)
        {
            Id = id;
            RiderId = riderId;
            MotorcycleId = motorcycleId;
            StartDateUtc = startDateUtc;
            PlannedEndDateUtc = plannedEndDateUtc;
            PlanType = planType;
            DailyRate = dailyRate;
            Status = RentalStatus.Active;
            CreatedAtUtc = DateTime.UtcNow;
            TotalAmount = CalculatePlannedTotal();
        }

        public static Rental Create(Guid id, string riderId, string motorcycleId, DateTime startDateUtc, DateTime plannedEndDateUtc, PlanType planType, decimal dailyRate)
        {
            if (startDateUtc.Date < DateTime.UtcNow.Date)
                throw new ArgumentException("Start date cannot be in the past.", nameof(startDateUtc));
            if (plannedEndDateUtc.Date <= startDateUtc.Date)
                throw new ArgumentException("Planned end date must be after start date.", nameof(plannedEndDateUtc));
            if (dailyRate <= 0)
                throw new ArgumentException("Daily rate must be greater than zero.", nameof(dailyRate));

            return new Rental(id, riderId, motorcycleId, DateTime.SpecifyKind(startDateUtc.Date,DateTimeKind.Utc), DateTime.SpecifyKind(plannedEndDateUtc.Date,DateTimeKind.Utc), planType, dailyRate);
        }

        private decimal CalculatePlannedTotal()
        {
            var days = (PlannedEndDateUtc.Date - StartDateUtc.Date).Days + 1;
            return days * DailyRate;
        }

        public void MarkReturned(DateTime returnedAtUtc, decimal finalTotal)
        {
            if(Status != RentalStatus.Active)
                throw new InvalidOperationException("Only active rentals can be marked as returned.");

            EndDateUtc = returnedAtUtc.Date;
            TotalAmount = finalTotal;
            Status = RentalStatus.Returned;
        }

        public void Cancel()
        {
            if (Status != RentalStatus.Active)
                throw new InvalidOperationException("Rental is not active.");

            Status = RentalStatus.Canceled;
        }
    }
}
