using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NextNews.Data;
using NextNews.Services;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder =>
    builder.AddJsonFile("local.settings.json", true, true)
    )
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection")));
    })
    .Build();

host.Run();
