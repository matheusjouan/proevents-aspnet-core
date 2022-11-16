using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEvents.Core.Entities;

namespace ProEvents.Infrastructure.Persistence.EntityConfiguration;
public class SpeakerConfiguration : IEntityTypeConfiguration<Speaker>
{
    public void Configure(EntityTypeBuilder<Speaker> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Resume)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(200);

        builder.HasMany(s => s.SocialNetworks)
            .WithOne(sn => sn.Speaker)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
