namespace Mottu.RentalApp.CrossCutting.Settings
{
    public class MinioSettings
    {
        public string Endpoint { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string PublicBaseUrl { get; set; } = string.Empty;
    }
}