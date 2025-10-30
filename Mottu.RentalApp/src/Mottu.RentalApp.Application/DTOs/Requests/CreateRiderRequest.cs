using Mottu.RentalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs.Requests
{
    public class CreateRiderRequest
    {
        public Guid? Id { get; init; }
        public string Name { get; init; } = default!;
        public string Cnpj { get; init; } = default!;
        public DateTime BirthDate { get; init; }
        public string CnhNumber { get; init; } = default!;
        public CnhType CnhType { get; init; } 
        
    }
}
