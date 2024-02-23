using Asp.Versioning;
using CityInfo.API.ActionFilters;
using CityInfo.API.Authorization.Handlers;
using CityInfo.API.Contracts;
using CityInfo.API.Data;
using CityInfo.API.Models;
using CityInfo.API.Repository;
using CityInfo.API.Repository.Implementors;
using CityInfo.API.Services;
using CityInfo.API.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace CityInfo.API;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("Logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog();

        builder.Services.AddResponseCaching();

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;

            options.Filters.Add(new AuthorizeFilter());
            options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

            options.CacheProfiles.Add("240SecondsCacheProfile",
                new CacheProfile
                {
                    Duration = 240
                });

        })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            })
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
            .AddXmlDataContractSerializerFormatters()
        .ConfigureApiBehaviorOptions(setupAction =>
        {
            setupAction.InvalidModelStateResponseFactory = context =>
            {
                var problemDetailsFactory = context.HttpContext.RequestServices
                .GetRequiredService<ProblemDetailsFactory>();

                var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext,
                    context.ModelState);

                validationProblemDetails.Detail = "See the errors field for details";
                validationProblemDetails.Instance = context.HttpContext.Request.Path;

                var actionExecutingContext = context as ActionExecutingContext;
                if ((context.ModelState.ErrorCount > 0)
                    && (actionExecutingContext?.ActionArguments.Count ==
                    context.ActionDescriptor.Parameters.Count))
                {
                    validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                    validationProblemDetails.Title = "One or more validation errors occurred";

                    return new UnprocessableEntityObjectResult(validationProblemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                }

                validationProblemDetails.Status = StatusCodes.Status400BadRequest;
                validationProblemDetails.Title = "One or more error on input occurred";
                return new BadRequestObjectResult(validationProblemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        });


        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
        var connectionString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddCors();

        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<IPointOfInterestRepository, PointOfInterestRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<CityExistsFilterAttribute>();
        builder.Services.AddScoped<ValidationFilterAttribute>();
        builder.Services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, CityAuthorizationCrudHandler>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<SeedRolesService>();
        builder.Services.AddTransient<ConfigureSwaggerOptions>();

        var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            //options.ApiVersionReader = ApiVersionReader.Combine(
            //    new QueryStringApiVersionReader("api-version"),
            //    new HeaderApiVersionReader("X-Version"));
        });

        apiVersioningBuilder.AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
                    ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
                };
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("RequireAdmin", policy =>
                policy.RequireRole("Admin"))
            .AddPolicy("RequireUser", policy =>
                policy.RequireRole("User"));

        var configureSwaggerOptionsService = builder.Services.BuildServiceProvider()
            .GetRequiredService<ConfigureSwaggerOptions>();

        builder.Services.AddSwaggerGen(options =>
        {
            configureSwaggerOptionsService.Configure(options);
            options.OperationFilter<SwaggerDefaultValues>();

            options.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "CityInfoApiBearerAuth"
                        }
                    },
                    new List<string>()
                }
            });

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var fullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(fullPath);

        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }
        else
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An Unexpected fault happened, try again later.");
                });
            });

        using (var scope = app.Services.CreateScope())
        {
            var seedRolesService = scope.ServiceProvider.GetRequiredService<SeedRolesService>();
            seedRolesService.SeedRoles().Wait();
        }

        app.UseCors(options => options
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()
        );

        app.UseHttpsRedirection();

        app.UseResponseCaching();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
