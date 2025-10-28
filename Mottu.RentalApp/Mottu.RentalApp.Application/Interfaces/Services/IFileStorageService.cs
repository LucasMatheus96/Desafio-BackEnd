using Microsoft.AspNetCore.Http;


namespace Mottu.RentalApp.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadCnhImageAsync(string riderId, IFormFile file);
    }
}
