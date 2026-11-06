using System.ComponentModel.DataAnnotations;

namespace IntelligentNewsAnalyzer.Models;

public class ArticleAnalysis
{
    public int Id { get; set; }

    public int? ArticleId { get; set; }
    public Article? Article { get; set; }

    [Required, MaxLength(500)]
    public string Title { get; set; } = string.Empty;

    public string OriginalText { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string KeyPoints { get; set; } = string.Empty;

    public string MainIdea { get; set; } = string.Empty;

    [MaxLength(16)]
    public string Language { get; set; } = "en";

    [MaxLength(80)]
    public string Category { get; set; } = "general";

    [MaxLength(500)]
    public string Topics { get; set; } = string.Empty;

    [MaxLength(40)]
    public string Sentiment { get; set; } = "neutral";

    [MaxLength(40)]
    public string Engine { get; set; } = "fallback";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
