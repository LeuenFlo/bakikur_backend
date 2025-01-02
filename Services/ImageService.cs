using Microsoft.AspNetCore.Hosting;

namespace BakikurBackend.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ImageService> _logger;

    public ImageService(
        IWebHostEnvironment environment,
        ILogger<ImageService> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        try
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving image");
            throw;
        }
    }

    public async Task DeleteImageAsync(string imageUrl)
    {
        try
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return;
            }

            var fileName = Path.GetFileName(imageUrl);
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                _logger.LogInformation("Deleted image: {ImageUrl}", imageUrl);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting image: {ImageUrl}", imageUrl);
            throw;
        }
    }
} 