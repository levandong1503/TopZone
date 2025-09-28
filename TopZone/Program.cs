using Application.extension;
using Infrastructure.Data;
using Infrastructure.Extension;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TopZone.Mapper;
using TopZone.Middlewares;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("TopZoneDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<TopZoneContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });

        //auto mapper
        builder.Services.AddAutoMapper(ctf => { },typeof(MapperConfigurationProfile).Assembly);
        //OR: builder.Services.AddAutoMapper(new[] { typeof(MapperConfigurationProfile).Assembly });

        // config service
        builder.Services.InfrastructureRegisterService();
        builder.Services.ApplicationRegisterServices();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();

        builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
        {
            // Default Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<TopZoneContext>();

        //builder.Services.Configure<IdentityOptions>(options =>
        //{
        //    // Default Password settings.
        //    options.Password.RequireDigit = false;
        //    options.Password.RequireLowercase = false;
        //    options.Password.RequireNonAlphanumeric = false;
        //    options.Password.RequireUppercase = false;
        //    options.Password.RequiredLength = 6;
        //    options.Password.RequiredUniqueChars = 1;
        //});

        builder.Services
            .AddControllers()
            .AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
                 Description = "Enter 'Bearer {token}'",
                 Name = "Authorization",
                 In = ParameterLocation.Header,
                 Type = SecuritySchemeType.ApiKey,
                 Scheme = "Bearer"
             });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                    }, new string[]{}
                }
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandleMiddleware>();

        app.MapIdentityApi<ApplicationUser>();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}