using Mottu.RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task AddAsync(Rental rental);
        Task<Rental?> GetByIdAsync(Guid id);
        Task<bool> MotorcycleHasActiveRentalAsync(Guid motorcycleId);
        Task UpdateAsync(Rental rental);
    }
}
