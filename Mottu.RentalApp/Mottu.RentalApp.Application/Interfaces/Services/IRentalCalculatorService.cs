using Mottu.RentalApp.Application.DTOs;
using Mottu.RentalApp.Domain.Enums;


namespace Mottu.RentalApp.Application.Interfaces.Services
{
    public interface IRentalCalculatorService
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
        }
    }
}
