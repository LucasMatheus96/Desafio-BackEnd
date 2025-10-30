using Mottu.RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Interfaces.Repositories
{
    public interface IRiderRepository
    {
        Task AddAsync(Rider rider);
        Task<Rider?> GetByIdAsync(Guid id);
        Task<Rider?> GetByCnpjAsync(string cnpj);
        Task<Rider?> GetByCnhNumberAsync(string cnhNumber);
        Task UpdateAsync(Rider rider);
    }
}
