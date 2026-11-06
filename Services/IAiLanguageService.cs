using IntelligentNewsAnalyzer.Models;

namespace IntelligentNewsAnalyzer.Services;

public interface IAiLanguageService
{
    bool IsConfigured { get; }
    Task<AiAnalysisResult> AnalyzeAsync(string title, string text, CancellationToken cancellationToken = default);
}
