using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Models.Entities;

namespace NotesAPI.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes{ get; set; }
        public DbSet<NotesGroup> NotesGroups { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }




    }
}
