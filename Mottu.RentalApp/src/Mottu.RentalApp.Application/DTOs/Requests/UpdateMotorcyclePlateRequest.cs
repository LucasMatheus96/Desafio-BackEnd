using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Requests
{
    public class UpdateMotorcyclePlateRequest
    {
        [JsonPropertyName("placa")]
        public string Plate { get; init; } = default!;
        
    }
}
