using System.ComponentModel.DataAnnotations;
using IntelligentNewsAnalyzer.Models;

namespace IntelligentNewsAnalyzer.Models.ViewModels;

public class AnalyzeFormViewModel
{
    [Display(Name = "Заглавие")]
    [MaxLength(500)]
    public string? Title { get; set; }

    [Display(Name = "Текст за анализ")]
    [Required(ErrorMessage = "Въведете текст или описание на новина.")]
    [MinLength(40, ErrorMessage = "Текстът трябва да е поне 40 символа.")]
    public string Text { get; set; } = string.Empty;

    public ArticleAnalysis? Result { get; set; }
    public IReadOnlyList<ArticleAnalysis> Recent { get; set; } = [];
}
