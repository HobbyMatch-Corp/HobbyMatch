using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Emit;
using System.Security.Claims;

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

			// Database seeding
			optionsBuilder.UseSeeding((context, _) =>
			{
				var user = context.Set<User>().FirstOrDefault(u => u.Email == "user@test.com");
				if (user == null)
				{

					var userManager = context.GetService<UserManager<Organizer>>();
					var newUser = new User
					{
						UserName = "TestUser",
						Email = "user@test.com"
					};

					userManager.CreateAsync(newUser, "Pass123!").Wait();
					userManager.AddClaimsAsync(newUser, new List<Claim>
					{
						new Claim(ClaimTypes.Name, "Test User")
					}).Wait();
				}

				var bclient = context.Set<BusinessClient>().FirstOrDefault(u => u.Email == "businessclient@test.com");
				if (bclient == null)
				{

					var userManager = context.GetService<UserManager<Organizer>>();

					var newBC = new BusinessClient
					{
						UserName = "TestBusinessClient",
						Email = "businessclient@test.com",
						TaxID = "taxid_2147743"
					};

					userManager.CreateAsync(newBC, "Pass123!").Wait();
					userManager.AddClaimsAsync(newBC, new List<Claim>
					{
						new Claim(ClaimTypes.Name, "Business Client")
					}).Wait();
				}
				var eventList = context.Set<Event>().ToList();
				if (eventList.Count() == 0)
				{
					var organizer = context.Set<User>().FirstOrDefault(u => u.Email == "user@test.com");
					if (organizer != null)
					{
						var newEvent = new Event
						{
							Organizer = organizer,
							StartTime = DateTime.UtcNow,
							EndTime = DateTime.UtcNow.AddDays(1),
							MaxUsers = 1,
							MinUsers = 1,
							Price = 1,
							Name = "Siata",
							Description = "Chcesz zagrać w siatę? Super, akurat się ciepło zrobiło. Zapraszamy na plażę w Kostaryce :).",
							Location = new LocationNullable()
						};
						context.Add(newEvent);
						context.SaveChanges();
					}
					else
					{
						Console.WriteLine("Seeding error: Organizer with the specified email does not exist.");
					}
				}
				context.SaveChanges();
			})
			.UseAsyncSeeding(async (context, _, ct) =>
			{
				var user = await context.Set<User>().FirstOrDefaultAsync(u => u.Email == "user@test.com", ct);
				if (user == null)
				{

					var userManager = context.GetService<UserManager<Organizer>>();
					var newUser = new User
					{
						UserName = "TestUser",
						Email = "user@test.com"
					};

					await userManager.CreateAsync(newUser, "Pass123!");
					await userManager.AddClaimsAsync(newUser, new List<Claim>
					{
						new Claim(ClaimTypes.Name, "Test User")
					});
				}

				var bclient = await context.Set<BusinessClient>().FirstOrDefaultAsync(u => u.Email == "businessclient@test.com");
				if (bclient == null)
				{

					var userManager = context.GetService<UserManager<Organizer>>();
					var newBC = new BusinessClient
					{
						UserName = "TestBusinessClient",
						Email = "businessclient@test.com",
						TaxID = "taxid_2147743"
					};

					await userManager.CreateAsync(newBC, "Pass123!");
					await userManager.AddClaimsAsync(newBC, new List<Claim>
					{
						new Claim(ClaimTypes.Name, "Business Client")
					});
				}
				var eventList = await context.Set<Event>().ToListAsync();
				if (eventList.Count == 0)
				{
					var organizer = await context.Set<User>().FirstOrDefaultAsync(u => u.Email == "user@test.com");
					if (organizer != null)
					{
						var newEvent = new Event
						{
							Organizer = organizer,
							StartTime = DateTime.UtcNow,
							EndTime = DateTime.UtcNow.AddDays(1),
							MaxUsers = 1,
							MinUsers = 1,
							Price = 1,
							Name = "Siata",
							Description = "Chcesz zagrać w siatę? Super, akurat się ciepło zrobiło. Zapraszamy na plażę w Kostaryce :).",
							Location = new LocationNullable()
						};
						await context.AddAsync(newEvent);
					}
					else
					{
						Console.WriteLine("Seeding error: Organizer with the specified email does not exist.");
					}
				}
				await context.SaveChangesAsync();
			});
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
