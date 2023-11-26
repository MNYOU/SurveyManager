using System.Reflection;
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
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            // this defines a CORS policy called "default"
            options.AddPolicy(Cors.Policy, policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        
        // services.AddTaskTracking(Configuration);

        // services.AddAuthorization(); // use asp.net identity
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                Configuration.Bind("JWTSettings", options);
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
        app.UseHttpLogging();
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseHttpsRedirection();
        }

        var basePath = configuration.GetValue<string>("BasePath");
        if (!string.IsNullOrWhiteSpace(basePath))
            app.UsePathBase(basePath);

        app.UseCors(Cors.Policy);
        app.UseRouting();

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseAuthentication();
        app.UseAuthorization();

        //app.UseMustChangePassword();

        app.UseResponseCaching();

        app.UseStaticFiles();

        var moduleInitializers = app.ApplicationServices.GetServices<IModuleInitializer>();
        // foreach (var moduleInitializer in moduleInitializers)
            // moduleInitializer.ConfigureServices(app.se);
        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapGraphQL("/graphql");
            endpoints.MapControllers()/*.RequireAuthorization()*/;
            // endpoints.MapRazorPages();
            // endpoints.MapBlazorHub();
            // endpoints.MapFallbackToPage("/_Host");
        });
    }
}