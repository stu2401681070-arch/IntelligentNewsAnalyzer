using System.ComponentModel.DataAnnotations;

namespace IntelligentNewsAnalyzer.Models;

public class Article
{
    public int Id { get; set; }

    [Required, MaxLength(500)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public string? Content { get; set; }

    [MaxLength(1000)]
    public string Url { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? ImageUrl { get; set; }

    [MaxLength(200)]
    public string SourceName { get; set; } = "Неизвестен източник";

    [MaxLength(200)]
    public string? Author { get; set; }

    [MaxLength(80)]
    public string Category { get; set; } = "general";

    [MaxLength(16)]
    public string Language { get; set; } = "en";

    public DateTime? PublishedAt { get; set; }

    public DateTime FetchedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ArticleAnalysis> Analyses { get; set; } = new List<ArticleAnalysis>();
}
