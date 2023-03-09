using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesAPI.Models.Entities;

namespace NotesAPI.Models.EntitiesConfiguration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(n => n.Title).IsRequired().HasMaxLength(50);

            builder.HasOne(n => n.Creator)
                .WithMany(u => u.Notes)
                .HasForeignKey(u => u.CreatorId);

            builder.HasMany(n => n.Users)
                .WithMany(u => u.Notes)
                .UsingEntity(j => j.ToTable("UsersNotes"));

            builder.HasMany(n => n.NotesGroups)
                .WithMany(ng => ng.Notes)
                .UsingEntity(j => j.ToTable("NoteGroupsNotes"));

        }
    }
}
