using HobbyMatch.Domain.Entities;
using HobbyMatch.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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

                var bclient = context.Set<User>().FirstOrDefault(u => u.Email == "businessclient@test.com");
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

                var bclient = await context.Set<User>().FirstOrDefaultAsync(u => u.Email == "businessclient@test.com");
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
            });
        }
    }
}
