using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Infrastructure.Common.Email;
using Infrastructure.Common.Logging;
using Infrastructure.Modules;
using Infrastructure.Services;
using Infrastructure.Services.Auth;
using Microsoft.Build.Locator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddMediator(x =>
        // {
        // });
        // services.AddScoped<IServiceBus, ServiceBusMediator>();
        //services.AddModules();
        services.AddModulesServices(configuration);

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(Email).Assembly);
        });
        
        services.AddScoped<IAuthConfigBuilder, JwtAuthConfigBuilder>();
        
        services.AddSingleton<IMessageService, EmailService>();
        services.AddSingleton<ICustomLogger, ConsoleLogger>();
    }
    
    private static void AddModulesServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services, configuration);
        return;
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var assemblyFolder = Path.GetDirectoryName(assemblyLocation);
        var files = Directory.GetFiles(assemblyFolder, "*.Startup.dll");
        var loadedStartupProjects = files.Select(Assembly.LoadFile).ToList();
        var moduleInitializerTypes = loadedStartupProjects
            .Select(x => x.GetTypes()
                .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t))
            )
            .Where(x => x is not null)
            .ToList();
        foreach (var moduleInitializerType in moduleInitializerTypes)
        {
            var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
            services.AddSingleton(typeof(IModuleInitializer), moduleInitializer);
            moduleInitializer.ConfigureServices(services, configuration);
        }
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetCallingAssembly();
        assembly = Assembly.GetExecutingAssembly();
        assembly = Assembly.GetEntryAssembly();
        if (assembly == null) return;
        var initializers = assembly
            .GetTypes()
            .Where(t => typeof(IModuleInitializer).IsAssignableFrom(t))
            .Select(t => (IModuleInitializer)Activator.CreateInstance(t));
        var type = typeof(IModuleInitializer);
        foreach (var moduleInitializer in initializers)
        {
            services.AddSingleton(type, moduleInitializer.GetType());
            moduleInitializer.ConfigureServices(services, configuration);
        }
    }
}