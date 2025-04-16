using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Data
{
    public class AppDbContext : IdentityDbContext<Organizer, IdentityRole<int>, int>
	{
		public AppDbContext() { }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<User> AppUsers { get; set; }
		public DbSet<BusinessClient> BusinessClients { get; set; }
		public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			DbSeeder.Seed(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity("BusinessClientEvent", b =>
			{
				b.HasOne("HobbyMatch.Domain.Entities.Event", null)
					.WithMany()
					.HasForeignKey("SponsoredEventsId")
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();

				b.HasOne("HobbyMatch.Domain.Entities.BusinessClient", null)
					.WithMany()
					.HasForeignKey("SponsorsPartnersId")
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();
			});

			modelBuilder.Entity("EventUser", b =>
			{
				b.HasOne("HobbyMatch.Domain.Entities.User", null)
					.WithMany()
					.HasForeignKey("SignUpListId")
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();

				b.HasOne("HobbyMatch.Domain.Entities.Event", null)
					.WithMany()
					.HasForeignKey("SignedUpEventsId")
					.OnDelete(DeleteBehavior.NoAction)
					.IsRequired();
			});

		}
	}
}
