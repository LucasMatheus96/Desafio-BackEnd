using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Responses
{
    public class RentalResponse
    {
        public Guid Id { get; set; }
        public string RiderId { get; set; } = default!;
        public string MotorcycleId { get; set; } = default!;
        public DateTime StartDateUtc { get; set; }
        public DateTime PlannedEndDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public string PlanType { get; set; } = default!;
        public decimal DailyRate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = default!;
    }
}
