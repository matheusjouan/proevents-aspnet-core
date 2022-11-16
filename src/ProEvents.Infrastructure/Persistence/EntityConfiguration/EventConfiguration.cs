using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEvents.Core.Entities;

namespace ProEvents.Infrastructure.Persistence.EntityConfiguration;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Place)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(e => e.AmountOfPeople)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(e => e.Phone)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(15);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(200);

        builder.Property(e => e.ImageURL)
            .HasColumnType("varchar")
            .HasMaxLength(500);

        builder.Property(e => e.Theme)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.HasMany(e => e.Batches)
            .WithOne(b => b.Event)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.SocialNetworks)
            .WithOne(s => s.Event)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
