using Microsoft.AspNetCore.Hosting;

namespace Application.Services.UploadImages
{
    public interface IUploadImageService
    {
        string? UploadImage(byte[] imageBytes, string imageName);
    }

    public class UploadImageService(IWebHostEnvironment webHostEnvironment) : IUploadImageService
    {
        public string? UploadImage(byte[] imageBytes, string imageName)
        {
            var guid = Guid.NewGuid();
            var extension = imageName[imageName.LastIndexOf('.')..];
            var imageUrl = Path.Combine(webHostEnvironment!.ContentRootPath, $"Uploads/{guid}", $"{guid}{extension}");
            var folderUrl = Path.Combine(webHostEnvironment!.ContentRootPath, $"Uploads/{guid}");

            try
            {
                if (!Path.Exists(folderUrl))
                {
                    Directory.CreateDirectory(folderUrl);
                }

                File.WriteAllBytes(imageUrl, imageBytes);
                return $"/Content/{guid}{extension}";
            }
            catch
            {
                throw;
            }
        }
    }
}