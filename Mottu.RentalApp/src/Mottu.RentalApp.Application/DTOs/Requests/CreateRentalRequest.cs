using Mottu.RentalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Requests
{
        public class CreateRentalRequest
        {
            public Guid Id { get; init; } = Guid.NewGuid();

            [JsonPropertyName("rider_id")]
            public Guid RiderId { get; init; }

            [JsonPropertyName("motorcycle_id")]
            public Guid MotorcycleId { get; init; }

            [JsonPropertyName("start_date")]
            public DateTime StartDate { get; init; }

            [JsonPropertyName("planned_end_date")]
            public DateTime PlannedEndDate { get; init; }

            [JsonPropertyName("plan_type")]
            public PlanType PlanType { get; init; }
        }    
}
