using System.ComponentModel.DataAnnotations;

namespace BakikurBackend.Models;

public class ProjectCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string Category { get; set; } = string.Empty;
    
    [Required]
    public DateTime CompletionDate { get; set; }
    
    [Required]
    public List<IFormFile> Images { get; set; } = new List<IFormFile>();
} 