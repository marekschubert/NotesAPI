using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using NotesAPI.Middleware;
using NotesAPI.Models;
using NotesAPI.Models.Entities;
using NotesAPI.Repository;
using NotesAPI.Repository.Implementations;
using NotesAPI.Repository.Interfaces;
using RestaurantAPI.Services.Implementations;
using RestaurantAPI.Services.Interfaces;
using System.Text;

namespace NotesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 
            
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            var authenticationSettings = new AuthenticationSettings();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

            builder.Services.AddSingleton(authenticationSettings);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });


            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            // Add services to the container.

            builder.Services.AddControllers().AddFluentValidation();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); //https://localhost:7037/swagger/index.html
            builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesApiDb")));
            
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<INotesGroupService, NotesGroupService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IPasswordHasher<NotesApiSeeder>, PasswordHasher<NotesApiSeeder>>();


            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserContextService, UserContextService>();

            builder.Services.AddScoped<NotesApiSeeder>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", options =>
                {
                    options.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(builder.Configuration["AllowedOrigins"]); // sekcja w appsettings.json, "AllowedOrigins": "http://localhost:8080"
                });
            });


            var app = builder.Build();
            
            app.UseMiddleware<ErrorHandlingMiddleware> ();

            app.UseHttpsRedirection();

            var scope = app.Services?.CreateScope();
            var seeder = scope?.ServiceProvider.GetRequiredService<NotesApiSeeder>();
            seeder?.Seed(); 

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("FrontEndClient");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}