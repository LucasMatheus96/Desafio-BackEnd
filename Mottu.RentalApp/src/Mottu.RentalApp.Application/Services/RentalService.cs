using AutoMapper;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Domain.Enums;


namespace Mottu.RentalApp.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRiderRepository _riderRepository;
        private readonly IRentalCalculatorService _calculator;
        private readonly IMapper _mapper;


        public RentalService(IRentalRepository rentalRepository, IMotorcycleRepository motorcycleRepository, IRiderRepository riderRepository, IRentalCalculatorService calculator, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _motorcycleRepository = motorcycleRepository;
            _riderRepository = riderRepository;
            _calculator = calculator;
            _mapper = mapper;
        }

        public async Task<RentalResponse> CreateRentalAsync(CreateRentalRequest createRentalRequest)
        {
            var rider = await _riderRepository.GetByIdAsync(createRentalRequest.RiderId) ?? throw new KeyNotFoundException("Rider not found.");
            if (!rider.HasCategoryA())
                throw new InvalidOperationException("Rider must have CNH category A to rent a motorcycle.");

            if (await _rentalRepository.MotorcycleHasActiveRentalAsync(createRentalRequest.MotorcycleId))
                throw new InvalidOperationException("Motorcycle already has an active rental.");

            decimal dailyRate = _calculator.GetDailyRate(createRentalRequest.PlanType);

            // start date validation: must be the first day after creation (as per spec)
            // here we assume startDate param is already validated by controller or higher layer

            var rental = Rental.Create(createRentalRequest.Id, createRentalRequest.RiderId, createRentalRequest.MotorcycleId, createRentalRequest.StartDate, createRentalRequest.PlannedEndDate, createRentalRequest.PlanType, dailyRate);
            await _rentalRepository.AddAsync(rental);

            return _mapper.Map<RentalResponse>(rental);
        }

        public async Task<Rental> ReturnRentalAsync(Guid rentalId, DateTime returnDate)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId) ?? throw new KeyNotFoundException("Rental not found.");
            if (rental == null) throw new KeyNotFoundException("Rental not found.");

            var calc = _calculator.CalculateFinalCharge(rental.PlanType, rental.StartDateUtc, rental.PlannedEndDateUtc, returnDate.Date, rental.DailyRate);
            rental.MarkReturned(returnDate, calc.Total);
            await _rentalRepository.UpdateAsync(rental);
            return rental;
        }
    }
}
