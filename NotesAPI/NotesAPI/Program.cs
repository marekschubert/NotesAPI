using Microsoft.EntityFrameworkCore;
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MainDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesApiDb")));
            builder.Services.AddScoped<INoteService, NoteService>();
            builder.Services.AddScoped<NotesApiSeeder>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();
            
            var scope = app.Services?.CreateScope();
            var seeder = scope?.ServiceProvider.GetRequiredService<NotesApiSeeder>();
            seeder?.Seed();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}