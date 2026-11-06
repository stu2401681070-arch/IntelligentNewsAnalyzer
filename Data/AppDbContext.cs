using IntelligentNewsAnalyzer.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelligentNewsAnalyzer.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleAnalysis> Analyses => Set<ArticleAnalysis>();
    public DbSet<SearchHistory> SearchHistory => Set<SearchHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasIndex(a => a.Url)
            .IsUnique();

        modelBuilder.Entity<Article>()
            .HasMany(a => a.Analyses)
            .WithOne(a => a.Article)
            .HasForeignKey(a => a.ArticleId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
