using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Responses
{
    public class UpdatePlateMotorcycleResponse
    {
        [JsonPropertyName("mensagem")]
        public string Message { get; init; } = default!;
    }
}
