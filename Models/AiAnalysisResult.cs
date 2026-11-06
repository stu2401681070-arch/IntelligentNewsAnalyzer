namespace IntelligentNewsAnalyzer.Models;

public class AiAnalysisResult
{
    public string Language { get; set; } = "en";
    public string Summary { get; set; } = string.Empty;
    public List<string> KeyPoints { get; set; } = [];
    public string Category { get; set; } = "general";
    public List<string> Topics { get; set; } = [];
    public string Sentiment { get; set; } = "neutral";
    public string MainIdea { get; set; } = string.Empty;
    public string Engine { get; set; } = "fallback";
}
