﻿using DomainServices.Repositories;
using Infrastructure.Services.DbInitializer;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IDbContextInitializer, DataContextInitializer>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<ISurveyRepository, SurveyRepository>();
    }
}