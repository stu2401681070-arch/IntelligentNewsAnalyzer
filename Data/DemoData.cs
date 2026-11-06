using IntelligentNewsAnalyzer.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelligentNewsAnalyzer.Data;

public static class DemoData
{
    public static async Task SeedIfEmptyAsync(AppDbContext db)
    {
        if (await db.Articles.AnyAsync())
        {
            return;
        }

        db.Articles.AddRange(
            new Article
            {
                Title = "AI chips drive a new wave of data-center investment",
                Description = "Technology firms expand computing capacity as demand for large language models continues to grow.",
                Content = "Cloud providers announced new data-center projects focused on training and inference for generative AI. Analysts say energy use and chip supply remain the main constraints.",
                Url = "https://example.com/ai-chips-data-centers",
                SourceName = "Tech Ledger",
                Category = "technology",
                Language = "en",
                PublishedAt = DateTime.UtcNow.AddHours(-5)
            },
            new Article
            {
                Title = "Central banks watch inflation while markets stay cautious",
                Description = "Investors wait for signals on interest rates after mixed economic data.",
                Content = "Equity markets moved in a narrow range as traders weighed slower inflation against weaker manufacturing figures.",
                Url = "https://example.com/markets-inflation",
                SourceName = "Finance Daily",
                Category = "business",
                Language = "en",
                PublishedAt = DateTime.UtcNow.AddHours(-8)
            },
            new Article
            {
                Title = "Scientists report progress on climate-resilient crops",
                Description = "A research consortium published field results for drought-tolerant wheat varieties.",
                Content = "Trials in three countries showed higher yields during dry seasons, according to the team. Further study is needed before wide commercial use.",
                Url = "https://example.com/climate-crops",
                SourceName = "Science Wire",
                Category = "science",
                Language = "en",
                PublishedAt = DateTime.UtcNow.AddHours(-12)
            },
            new Article
            {
                Title = "Championship final draws a record television audience",
                Description = "Broadcasters reported peak viewership during a tightly contested title match.",
                Content = "The home side won in extra time. Sponsors called the final one of the most watched sporting events of the year.",
                Url = "https://example.com/championship-final",
                SourceName = "Sports Desk",
                Category = "sports",
                Language = "en",
                PublishedAt = DateTime.UtcNow.AddHours(-20)
            }
        );

        await db.SaveChangesAsync();
    }
}
