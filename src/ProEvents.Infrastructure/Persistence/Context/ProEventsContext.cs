using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEvents.Core.Entities;
using ProEvents.Core.Identity.Model;
using ProEvents.Infrastructure.Persistence.EntityConfiguration;

namespace ProEvents.Infrastructure.Persistence.Context;
public class ProEventsContext : IdentityDbContext<User, Role, long,
    IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
    IdentityRoleClaim<long>, IdentityUserToken<long>>
{
	public ProEventsContext(DbContextOptions<ProEventsContext> options) : base(options) { }

	public DbSet<Event> Events { get; set; }
	public DbSet<Speaker> Speakers { get; set; }
	public DbSet<SpeakerEvent> SpeakersEvents { get; set; }
	public DbSet<Batch> Batches { get; set; }
	public DbSet<SocialNetwork> SocialNetworks { get; set; }

	protected override void OnModelCreating(ModelBuilder mb)
	{
        base.OnModelCreating(mb);
        mb.ApplyConfigurationsFromAssembly(typeof(EventConfiguration).Assembly);
	}
}
