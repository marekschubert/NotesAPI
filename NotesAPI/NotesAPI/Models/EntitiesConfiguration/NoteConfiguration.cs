using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesAPI.Models.Entities;
using NotesAPI.Models.Enums;

namespace NotesAPI.Models.EntitiesConfiguration
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(n => n.Title).IsRequired().HasMaxLength(50);

            /* builder.HasOne(n => n.Creator)
                 .WithMany(u => u.Notes)
                 .HasForeignKey(u => u.CreatorId);
            */
            builder.HasMany(n => n.Users)
                .WithMany(u => u.Notes)
                .UsingEntity<UsersNotes>(
                    x => x.HasOne(un => un.User)
                        .WithMany()
                        .HasForeignKey(un => un.UserId),

                    x => x.HasOne(un => un.Note)
                        .WithMany()
                        .HasForeignKey(un => un.NoteId),

                    x =>
                    {
                        x.HasKey(un => new { un.UserId, un.NoteId });
                        //x.Property(un => un.Role).HasDefaultValue(UserNoteRole.Viewer);
                    }

                );

            builder.HasMany(n => n.NotesGroups)
                .WithMany(ng => ng.Notes);
        }
    }
}
