using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProEvents.Core.Entities;

namespace ProEvents.Infrastructure.Persistence.EntityConfiguration;
public class SpeakerEventrConfiguration : IEntityTypeConfiguration<SpeakerEvent>
{
    public void Configure(EntityTypeBuilder<SpeakerEvent> builder)
    {
        builder.HasKey(SE => new { SE.EventId, SE.SpeakerId });
    }
}
