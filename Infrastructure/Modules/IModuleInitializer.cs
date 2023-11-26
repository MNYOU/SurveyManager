using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Modules;

public interface IModuleInitializer
{
    // TODO вынести реализацию из стартапа в класс, реализующий это 
    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}