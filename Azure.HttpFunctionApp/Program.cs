using Azure.HttpFunctionApp;
using Azure.HttpFunctionApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddScoped<ITestService, TestService>();
        s.AddDbContextPool<AppDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseNpgsql(
                Environment.GetEnvironmentVariable("SqlConnectionString"), npgOptions =>
                    npgOptions.MigrationsAssembly("Azure.HttpFunctionApp")
            );
        });
    })
    .Build();

host.Run();