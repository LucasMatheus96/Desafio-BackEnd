using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Responses
{
    public class MotorcycleResponse
    {
        public Guid Id { get; init; }

        [JsonPropertyName("identificador")]
        public string Identifier { get; init; } = default!;

        [JsonPropertyName("ano")]
        public int Year { get; init; }

        [JsonPropertyName("modelo")]
        public string Model { get; init; } = default!;

        [JsonPropertyName("placa")]
        public string Plate { get; init; } = default!;

        [JsonPropertyName("data de criação")]
        public DateTime CreatedAtUtc { get; init; }
    }
}
