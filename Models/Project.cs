using System.Text.Json.Serialization;

namespace BakikurBackend.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CompletionDate { get; set; }
    
    [JsonPropertyName("images")]
    public ICollection<ProjectImage> Images { get; set; } = new List<ProjectImage>();
} 