

using AutoMapper;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Domain.DomainEvents;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Domain.ValueObjects;
using System.Reflection;


namespace Mottu.RentalApp.Application.Services
{
    public class MotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IMapper _mapper;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository, IMessagePublisher messagePublisher, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _messagePublisher = messagePublisher;
            _mapper = mapper;
        }


        public async Task<MotorcycleResponse> CreateAsync(CreateMotorcycleRequest createMotorcycleRequest)
        {
            var licensePlate = LicensePlate.Create(createMotorcycleRequest.Plate);
            var id = Guid.NewGuid();

            if (await _motorcycleRepository.GetByPlateAsync(licensePlate.Value) != null)
                throw new InvalidOperationException("Plate already exists.");

            var motorcycle = Motorcycle.Create(id, createMotorcycleRequest.Year, createMotorcycleRequest.Model, licensePlate);

            await _motorcycleRepository.AddAsync(motorcycle);

            // Publish event (payload can be a dedicated DTO)
            var @event = new MotorcycleRegisteredDomainEvent(motorcycle.Id, motorcycle.Year, motorcycle.Model, LicensePlate.Create(motorcycle.Plate));
            await _messagePublisher.PublishMotorcycleRegisteredAsync(new
            {
                Id = @event.MotorcycleId,
                Year = @event.Year,
                Model = @event.Model,
                Plate = @event.Plate,
                OccurredAtUtc = @event.OccurredOn
            });

            return _mapper.Map<MotorcycleResponse>(motorcycle);            
        }

        public async Task<IEnumerable<MotorcycleResponse>> GetAllAsync(string? plateFilter)
        {
            var motorcycles = await _motorcycleRepository.ListAsync(plateFilter);       

            return _mapper.Map<IEnumerable<MotorcycleResponse>>(motorcycles);
        }

        public async Task UpdatePlateAsync(UpdateMotorcyclePlateRequest updateMotorcyclePlateRequest)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(updateMotorcyclePlateRequest.MotorcycleId)
                ?? throw new KeyNotFoundException("Motorcycle not found.");

            var licensePlate = LicensePlate.Create(updateMotorcyclePlateRequest.Plate);

            var existing = await _motorcycleRepository.GetByPlateAsync(licensePlate.Value);
            if (existing != null && existing.Id != motorcycle.Id)
                throw new InvalidOperationException("Plate already used by another motorcycle.");

            motorcycle.UpdatePlate(licensePlate);
            await _motorcycleRepository.UpdateAsync(motorcycle);
        }

        public async Task DeleteAsync(Guid motorcycleId)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(motorcycleId)
                ?? throw new KeyNotFoundException("Motorcycle not found.");

            if (await _motorcycleRepository.HasActiveRentalsAsync(motorcycleId))
                throw new InvalidOperationException("Motorcycle has active rentals and cannot be removed.");

            motorcycle.Remove();
            await _motorcycleRepository.DeleteAsync(motorcycle);
        }
    }
}
