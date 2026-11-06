using IntelligentNewsAnalyzer.Data;
using IntelligentNewsAnalyzer.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IntelligentNewsAnalyzer.Services;

public class StatisticsService
{
    private readonly AppDbContext _db;

    public StatisticsService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<StatisticsViewModel> BuildAsync(CancellationToken cancellationToken = default)
    {
        var articles = await _db.Articles.AsNoTracking().ToListAsync(cancellationToken);
        var analyses = await _db.Analyses.AsNoTracking().ToListAsync(cancellationToken);

        var topics = analyses
            .SelectMany(a => (a.Topics ?? string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .Where(t => t.Length > 2)
            .GroupBy(t => t, StringComparer.OrdinalIgnoreCase)
            .Select(g => new NamedCount { Name = g.First(), Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToList();

        return new StatisticsViewModel
        {
            ArticleCount = articles.Count,
            AnalysisCount = analyses.Count,
            TopSources = articles
                .GroupBy(a => a.SourceName)
                .Select(g => new NamedCount { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(8)
                .ToList(),
            TopCategories = articles
                .GroupBy(a => NewsCategories.Label(a.Category))
                .Select(g => new NamedCount { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList(),
            TopTopics = topics
        };
    }
}
