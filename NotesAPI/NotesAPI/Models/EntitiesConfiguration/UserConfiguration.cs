using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesAPI.Models.Entities;

namespace NotesAPI.Models.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(128);

            builder.Property(u => u.PasswordHash).IsRequired();



        }
    }
}
