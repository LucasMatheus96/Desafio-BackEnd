using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Infrastructure.Persistence.Context;

namespace Mottu.RentalApp.Infrastructure.Persistence.Repositories
{
    public class RiderRepository : IRiderRepository
    {
        private readonly RentalDbContext _db;

        public RiderRepository(RentalDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Rider rider)
        {
            await _db.Riders.AddAsync(rider);
            await _db.SaveChangesAsync();
        }

        public async Task<Rider?> GetByIdAsync(Guid id)
        {
            return await _db.Riders.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rider?> GetByCnpjAsync(string cnpj)
        {
            var normalized = cnpj?.Trim();
            return await _db.Riders.AsNoTracking().FirstOrDefaultAsync(r => r.Cnpj == normalized);
        }

        public async Task<Rider?> GetByCnhNumberAsync(string cnhNumber)
        {
            var normalized = cnhNumber?.Trim();
            return await _db.Riders.AsNoTracking().FirstOrDefaultAsync(r => r.CnhNumber == normalized);
        }

        public async Task UpdateAsync(Rider rider)
        {
           _db.Riders.Update(rider);
          await  _db.SaveChangesAsync();
           
        }
    }
}
