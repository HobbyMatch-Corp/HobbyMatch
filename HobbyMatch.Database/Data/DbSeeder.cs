using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace HobbyMatch.Database.Data
{
    public class DbSeeder
    {
        /* POMYSL NA TA KLASE
           Zrobić minimalny graf encji, który obejmuje większość przypadków
            Kolejność kroków:
            1. Stworzyc business klientów
            2. Stworzyc venue
            3. Przypisac venue do business klientow
            4. Stworzyc jakies eventy z tymi venue i klientami
            5. Stworzyc userow
            6. Stworzyc jakies eventy gdzie userzy sa organizatorami
            7. Pozapisywac userow na eventy
        */

        private struct MyStruct
        {
            public object DbSet;
            public Func<object, bool> Comparer;
            public ICollection<object> objects;
        }

        private List<BusinessClient> clients = new()
        {
            new BusinessClient() {Email = "bclient1@test.com", UserName="BusinessClient1", TaxID="taxId1_16498+544684",
                Venues=new[] { new Venue() {Name = "The Creative Canvas", Address = "123 Art Street, New York, NY", MaxUsers = 25, Price = 0,
                    Location = new Location() { Latitude = 40.7128, Longitude = -74.0060 }, Description = "A cozy art studio for painting and drawing enthusiasts. Offers guided sessions and open studio hours."
                }, new Venue() { Name = "Peak Climb Center", Address = "456 Mountain Ave, Denver, CO", MaxUsers = 100, Price = 50,
                    Location = new Location() { Latitude = 39.7392, Longitude = -104.9903 }, Description = "Indoor rock climbing gym with various difficulty levels, great for climbing hobbyists of all skill levels."
                },} },
            new BusinessClient() {Email = "bclient2@test.com", UserName="BusinessClient2", TaxID="taxId1_189481815+8787",
                Venues=new[] { new Venue() { Name = "Gamers' Haven", Address = "789 Pixel Blvd, Austin, TX", MaxUsers = 40, Price = 20,
                    Location = new Location() { Latitude = 30.2672, Longitude = -97.7431 }, Description = "A dedicated space for tabletop games, RPGs, and board game meetups. Snacks and drinks available."
                }, new Venue() { Name = "Strings & Things Music Hall", Address = "321 Melody Ln, Nashville, TN", MaxUsers = 60, Price = 35,
                    Location = new Location() { Latitude = 36.1627, Longitude = -86.7816 }, Description = "A venue for music lovers to jam, take lessons, or perform. Equipped with instruments and sound systems."
                }, new Venue() { Name = "Urban Garden Hub", Address = "159 Greenway Dr, Portland, OR", MaxUsers = 20, Price = 0,
                    Location = new Location() { Latitude = 45.5152, Longitude = -122.6784 }, Description = "Community garden and workshop space for gardening enthusiasts. Offers tools, soil, and seasonal classes."
                }} }
        };



        public void SetUpDbSeeding(DbContextOptionsBuilder optionsBuilder)
        {
            var v = clients[0].Venues.ElementAt(0);
            optionsBuilder
                .UseSeeding((context, _) =>
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
    }
}
