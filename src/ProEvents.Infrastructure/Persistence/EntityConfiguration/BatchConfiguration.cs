using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEvents.Core.Entities;

namespace ProEvents.Infrastructure.Persistence.EntityConfiguration;
public class BatchConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Price)
            .IsRequired()
            .HasColumnType("decimal")
            .HasPrecision(10, 2);

        builder.Property(b => b.Amount)
            .IsRequired()
            .HasColumnType("int");
    }
}
