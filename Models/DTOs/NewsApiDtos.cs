using System.Text.Json.Serialization;

namespace IntelligentNewsAnalyzer.Models.DTOs;

public class NewsApiResponse
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("totalResults")]
    public int TotalResults { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("articles")]
    public List<NewsApiArticle> Articles { get; set; } = [];
}

public class NewsApiArticle
{
    [JsonPropertyName("source")]
    public NewsApiSource? Source { get; set; }

    [JsonPropertyName("author")]
    public string? Author { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("urlToImage")]
    public string? UrlToImage { get; set; }

    [JsonPropertyName("publishedAt")]
    public DateTime? PublishedAt { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }
}

public class NewsApiSource
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
