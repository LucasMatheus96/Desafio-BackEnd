using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs
{   
    public class RentalCalculationResult
    {
        public decimal Total { get; init; }
        public int UsedDays { get; init; }
        public int RemainingDays { get; init; }
        public decimal Penalty { get; init; }
        public int ExtraDays { get; init; }
        public decimal ExtraCharge { get; init; }
    }

}
