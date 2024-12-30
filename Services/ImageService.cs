namespace BakikurBackend.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<ImageService> _logger;

    public ImageService(IWebHostEnvironment environment, ILogger<ImageService> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        try
        {
            // Create uploads directory if it doesn't exist
            var uploadsDirectory = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            // Generate unique filename
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

            // Save the file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            // Return the relative path
            return $"/uploads/{uniqueFileName}";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving image");
            throw;
        }
    }
} 