using IntelligentNewsAnalyzer.Models;

namespace IntelligentNewsAnalyzer.Services;

public class NewsFetchResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public IReadOnlyList<Article> Articles { get; init; } = [];
}

public interface INewsApiService
{
    bool IsConfigured { get; }
    Task<NewsFetchResult> FetchAsync(string? query, string? category, string language, CancellationToken cancellationToken = default);
}
