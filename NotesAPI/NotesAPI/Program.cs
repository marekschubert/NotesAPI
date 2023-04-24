using Microsoft.EntityFrameworkCore;
using NLog.Web;
using NotesAPI.Middleware;
using NotesAPI.Models;
using NotesAPI.Repository;
using NotesAPI.Repository.Implementations;
using NotesAPI.Repository.Interfaces;

namespace NotesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); //https://localhost:7037/swagger/index.html
            builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesApiDb")));
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddScoped<IUserService, UserService>();
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

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();

            var app = builder.Build();
            
            app.UseMiddleware<ErrorHandlingMiddleware> ();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}