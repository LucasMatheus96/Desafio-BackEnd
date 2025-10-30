using Minio;
using Minio.DataModel.Args;
using Mottu.RentalApp.Application.Interfaces.Services;
using Mottu.RentalApp.CrossCutting.Settings;


namespace Mottu.RentalApp.Infrastructure.Storage
{
    public class MinioFileStorageService : IFileStorageService
    {
        private readonly IMinioClient _minio;
        private readonly MinioSettings _settings;
        private const string Bucket = "cnh-images";

        public MinioFileStorageService(MinioSettings settings)
        {
            _settings = settings;
            _minio = new MinioClient()
                        .WithEndpoint(_settings.Endpoint)
                        .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                        .Build();
        }

        public async Task<string> UploadCnhImageAsync(string riderId, Stream fileStream, string contentType)
        {
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException(nameof(contentType));

            var ext = contentType switch
            {
                "image/png" => ".png",
                "image/bmp" => ".bmp",
                _ => throw new ArgumentException("Only png and bmp are allowed.")
            };

            // Ensure bucket exists
            var found = await _minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(Bucket));
            if (!found)
            {
                await _minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(Bucket));
            }

            var objectName = $"riders/{riderId}/cnh-{DateTime.UtcNow:yyyyMMddHHmmss}{ext}";

            var putArgs = new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minio.PutObjectAsync(putArgs);

            var url = $"{_settings.PublicBaseUrl.TrimEnd('/')}/{Bucket}/{objectName}";
            return url;
        }
    }
}
