using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntelligentNewsAnalyzer.Services;

public static class NewsCategories
{
    public static readonly IReadOnlyDictionary<string, string> Labels = new Dictionary<string, string>
    {
        ["general"] = "Общи",
        ["business"] = "Бизнес",
        ["entertainment"] = "Забавление",
        ["health"] = "Здраве",
        ["science"] = "Наука",
        ["sports"] = "Спорт",
        ["technology"] = "Технологии"
    };

    public static readonly IReadOnlyDictionary<string, string> Languages = new Dictionary<string, string>
    {
        ["en"] = "English",
        ["bg"] = "Български",
        ["de"] = "Deutsch",
        ["fr"] = "Français",
        ["es"] = "Español",
        ["it"] = "Italiano",
        ["pt"] = "Português",
        ["ru"] = "Русский",
        ["nl"] = "Nederlands",
        ["ar"] = "العربية",
        ["zh"] = "中文",
        ["he"] = "עברית",
        ["sv"] = "Svenska",
        ["no"] = "Norsk"
    };

    public static string Label(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return Labels["general"];
        }

        return Labels.TryGetValue(key, out var label) ? label : key;
    }

    public static string LanguageLabel(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return Languages["en"];
        }

        return Languages.TryGetValue(key, out var label) ? label : key;
    }

    public static IEnumerable<SelectListItem> SelectList(string? selected = null) =>
        Labels.Select(pair => new SelectListItem(pair.Value, pair.Key, pair.Key == selected));

    public static IEnumerable<SelectListItem> LanguageSelectList(string? selected = "en") =>
        Languages.Select(pair => new SelectListItem(pair.Value, pair.Key, pair.Key == selected));
}
