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


        [JsonPropertyName("entregador_id")]
        public string RiderId { get; init; } = default!;

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; init; } = default!;

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; init; }

        [JsonPropertyName("data_termino")]
        public DateTime PlannedEndDate { get; init; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime PlannedPreviousDate { get; init; }

        [JsonPropertyName("plano")]
        public PlanType PlanType { get; init; }
    }
}
