using System.Text.Json.Serialization;

namespace BakikurBackend.Models;

public class ProjectImage
{
    public int Id { get; set; }
    
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;
    
    [JsonIgnore]
    public int ProjectId { get; set; }
    
    [JsonIgnore]
    public Project Project { get; set; } = null!;
} 