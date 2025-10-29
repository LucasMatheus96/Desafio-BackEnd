using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.DTOs
{   
    public class RentalCalculationResult
    {
        public decimal Total { get; set; }
        public int UsedDays { get; set; }
        public int RemainingDays { get; set; }
        public decimal Penalty { get; set; }
        public int ExtraDays { get; set; }
        public decimal ExtraCharge { get; set; }
    }

}
