using IntelligentNewsAnalyzer.Models;

namespace IntelligentNewsAnalyzer.Models.ViewModels;

public class HomeDashboardViewModel
{
    public int ArticleCount { get; set; }
    public int AnalysisCount { get; set; }
    public int SourceCount { get; set; }
    public bool NewsApiConfigured { get; set; }
    public bool AiConfigured { get; set; }
    public IReadOnlyList<Article> LatestArticles { get; set; } = [];
    public IReadOnlyList<ArticleAnalysis> LatestAnalyses { get; set; } = [];
}
