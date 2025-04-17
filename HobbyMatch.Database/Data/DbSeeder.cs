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

        private List<BusinessClient> _clients = new()
        {
            new BusinessClient() { Email = "bclient1@test.com", UserName="BusinessClient1", TaxID="taxId1_16498+544684", // 0
                Venues=new[] { new Venue() {Name = "The Creative Canvas", Address = "123 Art Street, New York, NY", MaxUsers = 25, Price = 0, // 0
                    Location = new Location() { Latitude = 40.7128, Longitude = -74.0060 }, Description = "A cozy art studio for painting and drawing enthusiasts. Offers guided sessions and open studio hours."
                }, new Venue() { Name = "Peak Climb Center", Address = "456 Mountain Ave, Denver, CO", MaxUsers = 100, Price = 49.99M, // 1
                    Location = new Location() { Latitude = 39.7392, Longitude = -104.9903 }, Description = "Indoor rock climbing gym with various difficulty levels, great for climbing hobbyists of all skill levels."
                },} },
            new BusinessClient() {Email = "bclient2@test.com", UserName="BusinessClient2", TaxID="taxId1_189481815+8787", // 1
                Venues=new[] { new Venue() { Name = "Gamers' Haven", Address = "789 Pixel Blvd, Austin, TX", MaxUsers = 40, Price = 20, // 0
                    Location = new Location() { Latitude = 30.2672, Longitude = -97.7431 }, Description = "A dedicated space for tabletop games, RPGs, and board game meetups. Snacks and drinks available."
                }, new Venue() { Name = "Strings & Things Music Hall", Address = "321 Melody Ln, Nashville, TN", MaxUsers = 60, Price = 34.99M, // 1
                    Location = new Location() { Latitude = 36.1627, Longitude = -86.7816 }, Description = "A venue for music lovers to jam, take lessons, or perform. Equipped with instruments and sound systems."
                }, new Venue() { Name = "Urban Garden Hub", Address = "159 Greenway Dr, Portland, OR", MaxUsers = 20, Price = 0, // 2
                    Location = new Location() { Latitude = 45.5152, Longitude = -122.6784 }, Description = "Community garden and workshop space for gardening enthusiasts. Offers tools, soil, and seasonal classes."
                }} }
        };

        private List<Event> _events = new()
        {
            new Event() { // 0
                Name = "Sunset Sketch Session",
                Description = "An outdoor evening drawing meetup at the park. Bring your own supplies and enjoy nature-inspired art.",
                StartTime = DateTime.Now.AddDays(2).AddHours(17),
                EndTime = DateTime.Now.AddDays(2).AddHours(19),
                MinUsers = 2,
                MaxUsers = 15,
                Price = 9.99f,
                Location = new LocationNullable() { Latitude = 34.0522, Longitude = -118.2437 },
                Venue = null
            },

            new Event() { // 1
                Name = "Beachside Yoga Flow",
                Description = "Morning yoga session by the ocean to start your day fresh and relaxed. All levels welcome.",
                StartTime = DateTime.Now.AddDays(3).AddHours(7),
                EndTime = DateTime.Now.AddDays(3).AddHours(8),
                MinUsers = 1,
                MaxUsers = 20,
                Price = 11.99f,
                Location = new LocationNullable() { Latitude = 36.9741, Longitude = -122.0308 },
                Venue = null
            },

            new Event() { // 2
                Name = "Weekend Climb Challenge",
                Description = "Climbing competition for all skill levels. Prizes for top performers and refreshments included.",
                StartTime = DateTime.Now.AddDays(5).AddHours(10),
                EndTime = DateTime.Now.AddDays(5).AddHours(14),
                MinUsers = 10,
                MaxUsers = 50,
                Price = 25,
                Location = new LocationNullable { Latitude = null, Longitude = null }
            },

            new Event() { // 3
                Name = "Indie Jam Night",
                Description = "Open mic night for local musicians and bands to perform live. BYO instrument or use house gear.",
                StartTime = DateTime.Now.AddDays(1).AddHours(19),
                EndTime = DateTime.Now.AddDays(1).AddHours(22),
                MinUsers = 5,
                MaxUsers = 60,
                Price = 14.99f,
                Location = new LocationNullable { Latitude = null, Longitude = null }
            },

            new Event() { // 4
                Name = "Garden Prep & Seed Swap",
                Description = "Help prepare spring beds and exchange seeds with fellow gardeners. Tools and compost provided.",
                StartTime = DateTime.Now.AddDays(4).AddHours(10),
                EndTime = DateTime.Now.AddDays(4).AddHours(12),
                MinUsers = 3,
                MaxUsers = 20,
                Price = 5,
                Location = new LocationNullable { Latitude = null, Longitude = null }
            },
        };

        private List<User> _users;

        public DbSeeder()
        {
            _users = generateUsers(20);

            // Assign venue to event
            _events[2].Venue = _clients[0].Venues.ElementAt(1);
            _events[3].Venue = _clients[1].Venues.ElementAt(1);
            _events[4].Venue = _clients[1].Venues.ElementAt(2);

            // Assign organizer to event
            _events[0].Organizer = _users[0];
            _events[1].Organizer = _clients[1];
            _events[2].Organizer = _clients[0];
            _events[3].Organizer = _clients[1];
            _events[4].Organizer = _users[0];

            // Assign sponsors to event
            _events[0].SponsorsPartners = new List<BusinessClient>() { _clients[0], _clients[1] };
            _events[1].SponsorsPartners = new List<BusinessClient>() { _clients[1] };
            _events[2].SponsorsPartners = new List<BusinessClient>() { _clients[0]};
            _events[4].SponsorsPartners = new List<BusinessClient>() { _clients[1] };

            // Assign users to event
            _users.ForEach(u => u.SignedUpEvents = new List<Event>());
            _users[1..11].ForEach(u => u.SignedUpEvents.Add(_events[0]));
            _users.ForEach(u => u.SignedUpEvents.Add(_events[2]));
            _users[..12].ForEach(u => u.SignedUpEvents.Add(_events[3]));
            _users[..3].ForEach(u => u.SignedUpEvents.Add(_events[4]));

        }

        public void SetUpDbSeeding(DbContextOptionsBuilder optionsBuilder)
        {
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


        private List<User> generateUsers(int n)
        {
            var users = new List<User>();
            for (int i = 0; i < n; i++)
                users.Add(new User() { Email = $"user{i}@test.com", UserName = $"User {i}" });

            return users;
        }
    }
}
