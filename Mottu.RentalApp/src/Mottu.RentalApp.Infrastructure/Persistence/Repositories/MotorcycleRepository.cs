using Microsoft.EntityFrameworkCore;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Infrastructure.Persistence.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly RentalDbContext _db;
        public MotorcycleRepository(RentalDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Motorcycle motorcycle)
        {
            await _db.Motorcycles.AddAsync(motorcycle);
            await _db.SaveChangesAsync();
        }

        public async Task<Motorcycle?> GetByIdAsync(Guid id)
        {
            return await _db.Motorcycles.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id && !m.IsRemoved);
        }

        public async Task<Motorcycle?> GetByPlateAsync(string plate)
        {
            var normalized = plate?.Trim().ToUpperInvariant();
            return await _db.Motorcycles.AsNoTracking().FirstOrDefaultAsync(m => m.Plate == normalized && !m.IsRemoved);
        }

        public async Task<IEnumerable<Motorcycle>> ListAsync(string? plateFilter = null)
        {
            var query = _db.Motorcycles.AsQueryable().Where(m => !m.IsRemoved);
            if (!string.IsNullOrWhiteSpace(plateFilter))
            {
                var nf = plateFilter.Trim().ToUpperInvariant();
                query = query.Where(m => m.Plate.Contains(nf));
            }
            return await query.ToListAsync();
        }

        public Task UpdateAsync(Motorcycle motorcycle)
        {
            _db.Motorcycles.Update(motorcycle);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Motorcycle motorcycle)
        {
            // Soft-delete pattern: mark IsRemoved and save later
            motorcycle.Remove();
            _db.Motorcycles.Update(motorcycle);
            return Task.CompletedTask;
        }

        public async Task<bool> HasActiveRentalsAsync(Guid motorcycleId)
        {
            return await _db.Rentals.AnyAsync(r => r.MotorcycleId == motorcycleId && r.Status == Domain.Enums.RentalStatus.Active);
        }
    }
}
