using IntelligentNewsAnalyzer.Data;
using IntelligentNewsAnalyzer.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelligentNewsAnalyzer.Services;

public class AnalysisService
{
    private readonly AppDbContext _db;
    private readonly IAiLanguageService _ai;

    public AnalysisService(AppDbContext db, IAiLanguageService ai)
    {
        _db = db;
        _ai = ai;
    }

    public async Task<ArticleAnalysis> AnalyzeTextAsync(string title, string text, int? articleId = null, CancellationToken cancellationToken = default)
    {
        var result = await _ai.AnalyzeAsync(title, text, cancellationToken);

        var analysis = new ArticleAnalysis
        {
            ArticleId = articleId,
            Title = string.IsNullOrWhiteSpace(title) ? "Анализ на текст" : title.Trim(),
            OriginalText = text.Trim(),
            Summary = result.Summary,
            KeyPoints = string.Join("\n", result.KeyPoints.Where(p => !string.IsNullOrWhiteSpace(p))),
            MainIdea = result.MainIdea,
            Language = string.IsNullOrWhiteSpace(result.Language) ? "en" : result.Language.Trim().ToLowerInvariant(),
            Category = result.Category,
            Topics = string.Join(", ", result.Topics),
            Sentiment = result.Sentiment,
            Engine = result.Engine,
            CreatedAt = DateTime.UtcNow
        };

        if (articleId is int id)
        {
            var article = await _db.Articles.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (article is not null)
            {
                article.Category = result.Category;
                if (!string.IsNullOrWhiteSpace(result.Language))
                {
                    article.Language = result.Language.Trim().ToLowerInvariant();
                }
            }
        }

        _db.Analyses.Add(analysis);
        await _db.SaveChangesAsync(cancellationToken);
        return analysis;
    }

    public async Task<ArticleAnalysis?> AnalyzeArticleAsync(int articleId, CancellationToken cancellationToken = default)
    {
        var article = await _db.Articles.FirstOrDefaultAsync(a => a.Id == articleId, cancellationToken);
        if (article is null)
        {
            return null;
        }

        var text = string.Join("\n", new[] { article.Description, article.Content }
            .Where(s => !string.IsNullOrWhiteSpace(s)));

        if (string.IsNullOrWhiteSpace(text))
        {
            text = article.Title;
        }

        return await AnalyzeTextAsync(article.Title, text, article.Id, cancellationToken);
    }
}
