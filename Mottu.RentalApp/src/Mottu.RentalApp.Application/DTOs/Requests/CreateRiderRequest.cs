using Mottu.RentalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Requests
{
    public class CreateRiderRequest
    {
        [JsonPropertyName("identificador")]
        public string Identifier { get; init; } = default!;

        [JsonPropertyName("nome")]
        public string Name { get; init; } = default!;

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; init; } = default!;

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; init; }

        [JsonPropertyName("numero_cnh")]
        public string CnhNumber { get; init; } = default!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("tipo_cnh")]
        public CnhType CnhType { get; init; } 
        
    }
}
