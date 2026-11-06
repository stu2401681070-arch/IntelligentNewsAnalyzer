using DotNetEnv;
using IntelligentNewsAnalyzer.Data;
using IntelligentNewsAnalyzer.Services;
using Microsoft.EntityFrameworkCore;

static void LoadEnvFiles()
{
    var candidates = new[]
    {
        Path.Combine(Directory.GetCurrentDirectory(), ".env"),
        Path.Combine(AppContext.BaseDirectory, ".env"),
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".env")
    };

    foreach (var path in candidates)
    {
        var full = Path.GetFullPath(path);
        if (File.Exists(full))
        {
            Env.Load(full);
            return;
        }
    }
}

LoadEnvFiles();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<INewsApiService, NewsApiService>(client =>
{
    client.BaseAddress = new Uri("https://newsapi.org/v2/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHttpClient<IAiLanguageService, GeminiLanguageService>(client =>
{
    client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/v1beta/");
    client.Timeout = TimeSpan.FromSeconds(90);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<AnalysisService>();
builder.Services.AddScoped<StatisticsService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();
    await DemoData.SeedIfEmptyAsync(db);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
