using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Requests
{
    public class CreateMotorcycleRequest
    {
        public Guid? Id { get; set; }

        public string identificador { get; set; }
        public int Year { get; set; }
        public string Model { get; set; } = default!;
        public string Plate { get; set; } = default!;


    }
}
