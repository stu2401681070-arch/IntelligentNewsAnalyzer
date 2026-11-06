using System.ComponentModel.DataAnnotations;

namespace IntelligentNewsAnalyzer.Models;

public class SearchHistory
{
    public int Id { get; set; }

    [MaxLength(200)]
    public string Query { get; set; } = string.Empty;

    [MaxLength(80)]
    public string? Category { get; set; }

    [MaxLength(16)]
    public string? Language { get; set; }

    public int ResultCount { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
