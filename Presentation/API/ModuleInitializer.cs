using Application.Extensions;
using Backoffice.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Modules;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.Extensions;

namespace API;

public class ModuleInitializer: IModuleInitializer
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        
        // 
        // services.AddInfrastructureServices(configuration);
        services.AddApplicationServices(); // зачем это нужно
        services.AddPersistenceServices();
        services.AddBackofficeServices();
    }
}