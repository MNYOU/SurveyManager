using System.Net;
using System.Reflection;
using System.Text;
using Application.Extensions;
using Domain.Entities;
using Infrastructure.Common.Email;
using Infrastructure.Extensions;
using Infrastructure.Modules;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;

namespace API;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
    {
        Configuration = configuration;
        HostEnvironment = hostEnvironment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment HostEnvironment { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        // IdentityModelEventSource.ShowPII = true; что-то типо детальных логов ошибок
        // services.AddSingleton<IConfiguration>();
        services.AddInfrastructureServices(Configuration);
        services.AddHttpContextAccessor();
        
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });
        
        services.AddResponseCaching();

        // services.AddCustomSwagger(Configuration);
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });
        services.AddCors(options =>
        {
            // this defines a CORS policy called "default"
            options.AddPolicy(Cors.Policy, policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                // .AllowCredentials();
            });
        });

        
        // services.AddTaskTracking(Configuration);

        services.AddAuthorization(); // use asp.net identity
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            // .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = "front-end",
                    ValidIssuer = "https://localhost:7252",
                    IssuerSigningKey = new SymmetricSecurityKey("r38s3aio1a21ags2bm4GhrF9"u8.ToArray())
                };


                // options.TokenValidationParameters = new TokenValidationParameters()
                // {
                // IssuerSigningKey = new SymmetricSecurityKey("r38s3aio1a2.1ags2bm4GhrF9"u8.ToArray()),
                //         ValidateLifetime = true,
                //         
                // ValidateIssuer = true,
                //         // строка, представляющая издателя
                // ValidIssuer = "https://localhost:7252",
                // будет ли валидироваться потребитель токена
                // ValidateAudience = true,
                // установка потребителя токена
                // ValidAudience = "front-end",
                //         // будет ли валидироваться время существования
                //         // валидация ключа безопасности
                //         ValidateIssuerSigningKey = true,
                // };
                // Configuration.Bind("Authentication:JWTSettings", options);
            });
        // services.AddCustomAuthentication(Configuration);
        // services.AddCustomAuthorization(Configuration);
        
        services.AddLogging(config =>
        {
            config.AddDebug();
            config.AddConsole();
        });

        // services.AddAutoMapper();
        // services.addmediatr
        services.AddDbContext<DataContext>(opt =>
        {
            // opt.Options.fi
        });
        {
            // Identity
            // var builder = services.AddIdentityCore<User>(opt =>
            // {
            // });
            // var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            // identityBuilder.AddEntityFrameworkStores<DataContext> ();
            // identityBuilder.AddUserManager<AccountManager>();
            // identityBuilder.AddSignInManager<SignInManager<User>>();
        }
        
        // services.AddInfrastructure(Configuration);
        // services.AddGitLabAuthentication(Configuration.GetSection("Gitlab"));
        // services.AddKeycloak(Configuration.GetSection("Oidc"));

        //services.Configure<GitlabSettings>(Configuration.GetSection("Gitlab"));
        // services.Configure<OpenIdConnectOptions>(Configuration.GetSection("Oidc"));

        // services.AddSingleton<IRavenClient>(new RavenClient(Configuration["Sentry:Dsn"]));
        // services.AddScoped<SentryService>();

        // services.AddScoped<UserHelper>();

        // services.AddScoped<SlugRouteValueTransformer>();
        // services.AddSingleton(services);
        
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true); // TODO to each module?

        services.AddRazorPages();
        services.AddServerSideBlazor();
        // services.AddAntDesign();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        app.UseHsts();
        app.UseHttpLogging();
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }

        //// var basePath = configuration.GetValue<string>("BasePath");
        //// if (!string.IsNullOrWhiteSpace(basePath))
        ////     app.UsePathBase(basePath);

        app.UseCors(Cors.Policy);
        // app.UseStaticFiles();
        app.UseRouting();

        // app.UseForwardedHeaders(new ForwardedHeadersOptions
        // {
        //     ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        // });

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseResponseCaching();

        var moduleInitializers = app.ApplicationServices.GetServices<IModuleInitializer>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}