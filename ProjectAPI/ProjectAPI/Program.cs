using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using ProjectAPI;
using ProjectAPI.Application.MapperProfiles;
using ProjectAPI.Application.Services;
using ProjectAPI.Application.Services.Abstractions;
using ProjectAPI.Context;
using ProjectAPI.Domain.Repositories;
using ProjectAPI.Domain.UnitOfWork;
using ProjectAPI.Extension;
using ProjectAPI.Auth;
using ProjectAPI.Auth.Abstrations;
using ProjectAPI.Infrastructure.Repositories;
using ProjectAPI.Infrastructure.UnitOfWork;
using ProjectAPI.Model;
using System.Text;
using Microsoft.OpenApi.Models;

namespace MinimalAPI;

public class Program
{
    public static void Main(string[] args)
    {
        Logger logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectAPI", Version = "v1" });

            // Cấu hình JWT bearer
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Nhập token dạng: Bearer {your JWT token}"
            });

            // Gắn security vào tất cả endpoint
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.Configure<ConnectionStringSetting>(builder.Configuration.GetSection(nameof(ConnectionStringSetting)));
       
        //Nlog
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.Host.UseNLog();

        // App setting
        builder.Services.AddProjectAPISetting(builder.Configuration);

        // Auto mapper
        builder.Services.AddAutoMapper(typeof(TodoProfile));

        // DB context
        string connStr = builder.Configuration.GetSection(nameof(ConnectionStringSetting)).Get<ConnectionStringSetting>()?.DefaultConnection ?? "";
        builder.Services.AddDbContext<ToDoDBContext>(options => options.UseSqlServer(connStr));

        // Add DI
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IJwtTokenGenerator,JwtTokenGenerator>();
        builder.Services.AddScoped<ITodoService, TodoService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IProductService, ProductService>();

        //JWT
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? ""))
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                policy => policy
                    .WithOrigins("http://localhost:4200") // Angular đang chạy ở đây
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials() // nếu bạn dùng cookie hoặc auth
                    .SetIsOriginAllowed(origin => true)
            );
        });

        var app = builder.Build();

        app.UseCors("AllowAngularApp");

        app.UseHttpsRedirection();
        app.UseDeveloperExceptionPage();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}



