using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Infrastructure.Persistence.Context;

namespace Mottu.RentalApp.Infrastructure.Persistence.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalDbContext _db;

        public RentalRepository(RentalDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Rental rental)
        {
            await _db.Rentals.AddAsync(rental);
        }

        public async Task<Rental?> GetByIdAsync(Guid id)
        {
            return await _db.Rentals.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> MotorcycleHasActiveRentalAsync(Guid motorcycleId)
        {
            return await _db.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId && r.Status == Domain.Enums.RentalStatus.Active);
        }

        public Task UpdateAsync(Rental rental)
        {
            _db.Rentals.Update(rental);
            return Task.CompletedTask;
        }
    }
}
