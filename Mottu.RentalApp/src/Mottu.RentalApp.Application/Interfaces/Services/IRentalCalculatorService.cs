using Mottu.RentalApp.Application.DTOs;
using Mottu.RentalApp.Domain.Enums;


namespace Mottu.RentalApp.Application.Interfaces.Services
{
        public interface IRentalCalculatorService
        {
            RentalCalculationResult CalculateFinalCharge(
                PlanType plan,
                DateTime startDate,
                DateTime plannedEndDate,
                DateTime returnDate,
                decimal dailyRate);

            decimal GetDailyRate(PlanType plan);

        public static decimal GetEarlyPenaltyPercent(PlanType plan) => plan switch
        {
            PlanType.D7 => 0.20m,
            PlanType.D15 => 0.40m,
            _ => 0.00m
        };

        public const decimal LateDailySurcharge = 50.00m;

        public static int GetPlanDays(PlanType plan) => (int)plan;
    }    
}
