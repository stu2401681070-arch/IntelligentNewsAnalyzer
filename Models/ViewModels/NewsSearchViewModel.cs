using System.ComponentModel.DataAnnotations;
using IntelligentNewsAnalyzer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntelligentNewsAnalyzer.Models.ViewModels;

public class NewsSearchViewModel
{
    [Display(Name = "Ключова дума")]
    public string? Query { get; set; }

    [Display(Name = "Категория")]
    public string? Category { get; set; }

    [Display(Name = "Език")]
    public string Language { get; set; } = "en";

    public IReadOnlyList<Article> Articles { get; set; } = [];
    public string? Message { get; set; }
    public bool IsError { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; } = [];
    public IEnumerable<SelectListItem> Languages { get; set; } = [];
}
