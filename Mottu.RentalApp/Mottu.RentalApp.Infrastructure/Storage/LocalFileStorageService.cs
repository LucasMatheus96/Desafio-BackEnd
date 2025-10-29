using System;
using System.IO;
using System.Threading.Tasks;
using Mottu.RentalApp.Application.Interfaces.Services;

namespace Mottu.RentalApp.Infrastructure.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly string _root;

        public LocalFileStorageService(string rootPath = null)
        {
            _root = rootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(_root);
        }

        public async Task<string> UploadCnhImageAsync(string riderId, Stream fileStream, string contentType)
        {
            if (fileStream == null)
                throw new ArgumentNullException(nameof(fileStream));
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException(nameof(contentType));

            var ext = contentType switch
            {
                "image/png" => ".png",
                "image/bmp" => ".bmp",
                _ => throw new ArgumentException("Only png and bmp are allowed.")
            };

            var folder = Path.Combine(_root, "riders", riderId);
            Directory.CreateDirectory(folder);

            var fileName = $"cnh-{DateTime.UtcNow:yyyyMMddHHmmss}{ext}";
            var path = Path.Combine(folder, fileName);

            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(fs);

            return path;
        }
    }
}
