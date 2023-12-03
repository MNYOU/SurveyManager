using API;
using Infrastructure.Services.DbInitializer;

// TODO переделать структуру. что-то хранится тут, что-то там
public class Program
{
    public static async Task  Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await DatabaseInitialising(args, host);
        await host.RunAsync();
        // var builder = WebApplication.CreateBuilder(args);
        // var app = builder.Build();
        
// Add services to the container.

        // builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();

        // var app = builder.Build();

// Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
            // app.UseSwagger();
            // app.UseSwaggerUI();
        // }

        // app.UseHttpsRedirection();

        // app.UseAuthorization();

        // app.MapControllers();

        // app.Run();
    }

    public static async Task DatabaseInitialising(string[] args, IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                var dbInitializers = services.GetServices<IDbContextInitializer>();
                foreach (var dbInitializer in dbInitializers)
                {
                    await dbInitializer.Migrate();
                    await dbInitializer.Seed();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while running database migration.");
            }
        }
    }
    
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // webBuilder.UseStaticWebAssets();
                webBuilder.ConfigureLogging(logBuilder =>
                {
                    logBuilder.ClearProviders();
                    logBuilder.AddConsole();

                    logBuilder.SetMinimumLevel(LogLevel.Debug);

                });
                webBuilder.UseKestrel(options =>
                {
                    options.Limits.MaxRequestHeadersTotalSize = 1048576;
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}