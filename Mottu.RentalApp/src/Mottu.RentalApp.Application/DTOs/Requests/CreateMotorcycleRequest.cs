using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Mottu.RentalApp.Application.DTOs.Requests
{
    public class CreateMotorcycleRequest
    {
      
        [JsonPropertyName("identificador")]
        public string Identifier { get; set; } = default!;

        [JsonPropertyName("ano")]
        public int Year { get; set; }
     
        [JsonPropertyName("modelo")]
        public string Model { get; set; } = default!;

        [JsonPropertyName("placa")]
        public string Plate { get; set; } = default!;


    }
}
