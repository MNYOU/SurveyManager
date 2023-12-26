using Application.Mappings;
using Application.Services;
using Application.Services.Account;
using Application.Services.AdditionalRegistator;
using Application.Services.Super;
using Domain.Entities;
using DomainServices.Repositories;
using Infrastructure.Common.Auth;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Repositories;

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
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<IAuthenticationConfiguration, BaseAuthenticationConfiguration>();

        services.AddScoped<ISurveyService, SurveyService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAnalystService, AnalystService>();
        services.AddScoped<IAdditionalRegistrator, AdminRegistrator>();
        services.AddScoped<IAdditionalRegistrator, AnalystRegistrator>();
        services.AddScoped<IQuestionService, QuestionService>();

        services.AddScoped<ISuperAdminService, SuperAdminService>();
    }
}