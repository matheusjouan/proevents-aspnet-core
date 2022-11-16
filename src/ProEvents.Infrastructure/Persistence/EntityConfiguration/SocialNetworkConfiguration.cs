using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEvents.Core.Entities;

namespace ProEvents.Infrastructure.Persistence.EntityConfiguration;
public class SocialNetworkConfiguration : IEntityTypeConfiguration<SocialNetwork>
{
    public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(s => s.URL)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(200);
    }
}
