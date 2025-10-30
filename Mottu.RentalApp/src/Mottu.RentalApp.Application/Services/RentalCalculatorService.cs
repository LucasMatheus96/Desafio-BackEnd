using Mottu.RentalApp.Application.DTOs;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Services
{
    public class RentalCalculatorService : IRentalCalculatorService
    {
        public decimal GetDailyRate(PlanType plan)
        {
            return plan switch
            {
                PlanType.D7 => 30m,
                PlanType.D15 => 28m,
                PlanType.D30 => 22m,
                PlanType.D45 => 20m,
                PlanType.D50 => 18m,
                _ => throw new ArgumentOutOfRangeException(nameof(plan))
            };
        }

        public RentalCalculationResult CalculateFinalCharge(PlanType plan, DateTime startDate, DateTime plannedEndDate, DateTime returnDate, decimal dailyRate)
        {
            
            var start = startDate.Date;
            var plannedEnd = plannedEndDate.Date;
            var returned = returnDate.Date;

            var totalPlannedDays = (plannedEnd - start).Days + 1; 
            var usedDays = (returned - start).Days + 1;
            if (usedDays < 0) usedDays = 0;

            var result = new RentalCalculationResult();

            if (returned < plannedEnd)
            {
                
                var remainingDays = totalPlannedDays - usedDays;
                decimal penaltyPercent = plan switch
                {
                    PlanType.D7 => 0.20m,
                    PlanType.D15 => 0.40m,
                    _ => 0m
                };

                var penalty = remainingDays * dailyRate * penaltyPercent;
                var total = usedDays * dailyRate + penalty;

                result.Total = decimal.Round(total, 2);
                result.UsedDays = usedDays;
                result.RemainingDays = remainingDays;
                result.Penalty = decimal.Round(penalty, 2);
                result.ExtraDays = 0;
                result.ExtraCharge = 0m;
                return result;
            }
            else if (returned > plannedEnd)
            {
               
                var extraDays = (returned - plannedEnd).Days;
                var extraCharge = extraDays * 50m;
                var total = totalPlannedDays * dailyRate + extraCharge;

                result.Total = decimal.Round(total, 2);
                result.UsedDays = totalPlannedDays + extraDays;
                result.RemainingDays = 0;
                result.Penalty = 0m;
                result.ExtraDays = extraDays;
                result.ExtraCharge = decimal.Round(extraCharge, 2);
                return result;
            }
            else
            {
           
                var total = totalPlannedDays * dailyRate;
                result.Total = decimal.Round(total, 2);
                result.UsedDays = totalPlannedDays;
                result.RemainingDays = 0;
                result.Penalty = 0m;
                result.ExtraDays = 0;
                result.ExtraCharge = 0m;
                return result;
            }
        }
    }
}
