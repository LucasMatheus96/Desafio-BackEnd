using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Responses
{
    public class MotorcycleResponse
    {
        public Guid Id { get; init; }
        public int Year { get; init; }
        public string Model { get; init; } = default!;
        public string Plate { get; init; } = default!;
        public DateTime CreatedAtUtc { get; init; }
    }
}
