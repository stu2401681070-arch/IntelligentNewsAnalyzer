namespace IntelligentNewsAnalyzer.Models.ViewModels;

public class StatisticsViewModel
{
    public int ArticleCount { get; set; }
    public int AnalysisCount { get; set; }
    public IReadOnlyList<NamedCount> TopSources { get; set; } = [];
    public IReadOnlyList<NamedCount> TopCategories { get; set; } = [];
    public IReadOnlyList<NamedCount> TopTopics { get; set; } = [];
}

public class NamedCount
{
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
}
