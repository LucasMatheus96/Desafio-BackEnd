using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Interfaces.Services
{
    public interface IMotorcycleService
    {
        Task<MotorcycleResponse> CreateAsync(CreateMotorcycleRequest request);
        Task<IEnumerable<MotorcycleResponse>> GetAllAsync(string? plate);
        Task<MotorcycleResponse?> GetByIdAsync(string motorcycleIdentifier);
        Task<UpdatePlateMotorcycleResponse> UpdatePlateAsync(string motorcycleIdentifier, UpdateMotorcyclePlateRequest updateMotorcyclePlateRequest);
        Task DeleteAsync(string motorcycleIdentifier);

    }
}
