using AutoMapper;
using Mottu.RentalApp.Application.DTOs.Requests;
using Mottu.RentalApp.Application.DTOs.Responses;
using Mottu.RentalApp.Application.Interfaces.Repositories;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.Domain.Entities;
using Mottu.RentalApp.Domain.Enums;
using Mottu.RentalApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Application.Services
{
    public class RiderService : IRiderService
    {
        private readonly IRiderRepository _riderRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public RiderService(IRiderRepository riderRepository, IFileStorageService fileStorageService, IMapper mapper)
        {
            _riderRepository = riderRepository;
            _fileStorageService = fileStorageService;
            _mapper = mapper; 
        }

        public async Task<RiderResponse> CreateAsync(CreateRiderRequest createRiderRequest)
        {
         
            var id = Guid.NewGuid();
            var cnpjVo = Cnpj.Create(createRiderRequest.Cnpj);

            if (await _riderRepository.GetByCnpjAsync(cnpjVo.Value) != null)
                throw new InvalidOperationException("CNPJ already registered.");

            if (await _riderRepository.GetByCnhNumberAsync(createRiderRequest.CnhNumber) != null)
                throw new InvalidOperationException("CNH number already registered.");           

            var rider = Rider.Create(id, createRiderRequest.Identifier, createRiderRequest.Name, cnpjVo, createRiderRequest.BirthDate, createRiderRequest.CnhNumber, createRiderRequest.CnhType);
            await _riderRepository.AddAsync(rider);

            return _mapper.Map<RiderResponse>(rider) ;
        }

        public async Task<UploadCnhResponse> UploadCnhImageAsync(Guid riderId, UploadCnhRequest uploadCnhRequest)
        {
            var rider = await _riderRepository.GetByIdAsync(riderId)
                ?? throw new KeyNotFoundException("Rider not found.");

            if(string.IsNullOrWhiteSpace(uploadCnhRequest.ImagemCnh))
                throw new ArgumentException("CNH image (base64) is required.", nameof(uploadCnhRequest.ImagemCnh));

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(uploadCnhRequest.ImagemCnh);
            }
            catch (FormatException)
            {
                throw new Exception("Invalid base64 string.");
            }

            using var stream = new MemoryStream(imageBytes);

            var url = await _fileStorageService.UploadCnhImageAsync(riderId.ToString(), stream, "image/png");
            rider.UpdateCnhImage(url);
            await _riderRepository.UpdateAsync(rider);

             return new UploadCnhResponse(url, DateTime.UtcNow);
        }
    }
}
