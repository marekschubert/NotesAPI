using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesAPI.Models.Entities;

namespace NotesAPI.Models.EntitiesConfiguration
{
    public class NotesGroupConfiguration : IEntityTypeConfiguration<NotesGroup>
    {
        public void Configure(EntityTypeBuilder<NotesGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(ng => ng.Users)
                .WithMany(u => u.NotesGroups);
                

        }
    }
}
