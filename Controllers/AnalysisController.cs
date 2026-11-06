using IntelligentNewsAnalyzer.Data;
using IntelligentNewsAnalyzer.Models.ViewModels;
using IntelligentNewsAnalyzer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntelligentNewsAnalyzer.Controllers;

public class AnalysisController : Controller
{
    private readonly AppDbContext _db;
    private readonly AnalysisService _analysis;
    private readonly IAiLanguageService _ai;

    public AnalysisController(AppDbContext db, AnalysisService analysis, IAiLanguageService ai)
    {
        _db = db;
        _analysis = analysis;
        _ai = ai;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await BuildForm(null, cancellationToken));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AnalyzeFormViewModel form, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            form.Recent = await RecentAsync(cancellationToken);
            return View(form);
        }

        var title = string.IsNullOrWhiteSpace(form.Title) ? "Собствен текст" : form.Title.Trim();
        var result = await _analysis.AnalyzeTextAsync(title, form.Text, cancellationToken: cancellationToken);
        var model = await BuildForm(result, cancellationToken);
        model.Title = form.Title;
        model.Text = form.Text;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var analysis = await _db.Analyses
            .Include(a => a.Article)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (analysis is null)
        {
            return NotFound();
        }

        ViewBag.AiConfigured = _ai.IsConfigured;
        return View(analysis);
    }

    private async Task<AnalyzeFormViewModel> BuildForm(Models.ArticleAnalysis? result, CancellationToken cancellationToken)
    {
        return new AnalyzeFormViewModel
        {
            Result = result,
            Recent = await RecentAsync(cancellationToken)
        };
    }

    private Task<List<Models.ArticleAnalysis>> RecentAsync(CancellationToken cancellationToken) =>
        _db.Analyses.AsNoTracking()
            .OrderByDescending(a => a.CreatedAt)
            .Take(8)
            .ToListAsync(cancellationToken);
}
