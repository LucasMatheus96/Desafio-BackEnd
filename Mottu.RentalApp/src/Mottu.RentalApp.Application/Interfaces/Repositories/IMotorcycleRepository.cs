using Mottu.RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Interfaces.Repositories
{
    public interface IMotorcycleRepository
    {
        Task AddAsync(Motorcycle motorcycle);
        Task<Motorcycle?> GetByIdAsync(Guid id);
        Task<Motorcycle?> GetByPlateAsync(string plate);
        Task<IEnumerable<Motorcycle>> ListAsync(string? plateFilter = null);
        Task  UpdateAsync(Motorcycle motorcycle);
        Task DeleteAsync(Motorcycle motorcycle);
        Task<bool> HasActiveRentalsAsync(Guid motorcycleId);
    }
}
