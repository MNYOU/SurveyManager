using Application.Mappings;
using Application.Services;
using Domain.Entities;
using Infrastructure.Common.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(c =>
        {
            c.AddMaps(typeof(MappingProfile));
        });

        services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IAccountService<User>, AccountService>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IAuthenticationConfiguration, BaseAuthenticationConfiguration>();

        services.AddScoped<ISurveyService, SurveyService>();
        // services.AddScoped<IMessageService, em>();
    }
}