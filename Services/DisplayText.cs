namespace IntelligentNewsAnalyzer.Services;

public static class DisplayText
{
    public static string Sentiment(string? value) => value switch
    {
        "positive" => "Позитивен",
        "negative" => "Негативен",
        _ => "Неутрален"
    };

    public static string Engine(string? value) => value switch
    {
        "gemini" => "Google Gemini",
        "openai" => "Отдалечен езиков модел",
        "fallback-after-error" => "Локален резервен анализатор",
        _ => "Локален анализатор"
    };
}
