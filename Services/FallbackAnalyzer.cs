using System.Text.RegularExpressions;
using IntelligentNewsAnalyzer.Models;

namespace IntelligentNewsAnalyzer.Services;

public static class FallbackAnalyzer
{
    private static readonly Dictionary<string, string[]> CategoryKeywords = new()
    {
        ["business"] = ["market", "stock", "economy", "bank", "company", "trade", "finance", "бизнес", "пазар", "икономика"],
        ["entertainment"] = ["movie", "film", "music", "celebrity", "actor", "show", "филм", "музика"],
        ["health"] = ["health", "hospital", "doctor", "virus", "vaccine", "medical", "здраве", "болница"],
        ["science"] = ["research", "space", "scientist", "study", "nasa", "наука", "изследване"],
        ["sports"] = ["match", "goal", "team", "league", "championship", "спорт", "мач", "гол"],
        ["technology"] = ["ai", "software", "tech", "chip", "computer", "internet", "технологии", "софтуер"]
    };

    public static AiAnalysisResult Analyze(string title, string text)
    {
        var combined = $"{title}\n{text}";
        var summary = Summarize(text);
        var category = Classify(combined);
        var topics = ExtractTopics(combined);
        var language = DetectLanguage(combined);

        return new AiAnalysisResult
        {
            Language = language,
            Summary = summary,
            KeyPoints = topics.Take(3).Select(t => t).ToList(),
            Category = category,
            Topics = topics,
            Sentiment = DetectSentiment(combined),
            MainIdea = string.IsNullOrWhiteSpace(title) ? summary.Split('.').FirstOrDefault()?.Trim() ?? summary : title.Trim(),
            Engine = "fallback"
        };
    }

    public static string Summarize(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return "Няма достатъчно текст за резюме.";
        }

        var sentences = Regex.Split(text, @"(?<=[\.!\?])\s+")
            .Where(s => s.Trim().Length > 20)
            .Take(4)
            .ToArray();

        return sentences.Length == 0
            ? text.Trim()[..Math.Min(text.Trim().Length, 280)]
            : string.Join(" ", sentences);
    }

    private static string DetectLanguage(string text)
    {
        var cyrillic = Regex.Matches(text, @"[а-яА-ЯёЁ]").Count;
        var latin = Regex.Matches(text, @"[a-zA-Z]").Count;
        return cyrillic > latin ? "bg" : "en";
    }

    private static string Classify(string text)
    {
        var lower = text.ToLowerInvariant();
        var best = "general";
        var bestScore = 0;

        foreach (var (category, keywords) in CategoryKeywords)
        {
            var score = keywords.Count(k => lower.Contains(k));
            if (score > bestScore)
            {
                bestScore = score;
                best = category;
            }
        }

        return best;
    }

    private static List<string> ExtractTopics(string text)
    {
        var stop = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "the","and","for","with","that","this","from","have","will","are","was","were","been","not","but",
            "you","your","our","their","they","his","her","its","about","after","over","into","than","then",
            "said","says","just","more","also","can","has","had","who","what","when","where","which",
            "и","на","за","от","с","в","по","да","е","са","се","ще","не","като","или","че"
        };

        return Regex.Matches(text.ToLowerInvariant(), @"[a-zа-яё]{4,}")
            .Select(m => m.Value)
            .Where(w => !stop.Contains(w))
            .GroupBy(w => w)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => char.ToUpper(g.Key[0]) + g.Key[1..])
            .ToList();
    }

    private static string DetectSentiment(string text)
    {
        var lower = text.ToLowerInvariant();
        var positive = new[] { "win", "growth", "success", "record", "boost", "agree", "peace", "improve", "успех", "ръст" };
        var negative = new[] { "crisis", "death", "kill", "war", "crash", "loss", "fail", "attack", "risk", "криза", "война" };
        var pos = positive.Count(w => lower.Contains(w));
        var neg = negative.Count(w => lower.Contains(w));
        if (pos > neg) return "positive";
        if (neg > pos) return "negative";
        return "neutral";
    }
}
