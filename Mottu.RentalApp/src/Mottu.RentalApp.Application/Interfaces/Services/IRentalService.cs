using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using Mottu.RentalApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Interfaces.Services
{
    public interface IRentalService
    {
        Task<RentalResponse> CreateRentalAsync(CreateRentalRequest request);
    
        Task<Rental> ReturnRentalAsync(Guid rentalId, DateTime returnDate);
    }
}
