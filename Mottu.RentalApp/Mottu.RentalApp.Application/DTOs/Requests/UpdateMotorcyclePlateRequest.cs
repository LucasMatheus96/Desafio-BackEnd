using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Requests
{
    public class UpdateMotorcyclePlateRequest
    {
        public Guid MotorcycleId { get; init; }
        public string Plate { get; init; } = default!;

        public string OldPlate { get; init; } = default!;
    }
}
