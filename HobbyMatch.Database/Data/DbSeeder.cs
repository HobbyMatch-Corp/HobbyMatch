using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HobbyMatch.Database.Data;

public class DbSeeder
{
    private readonly List<BusinessClient> _clients = new()
    {
        new BusinessClient
        {
            Email = "bclient1@test.com", UserName = "BusinessClient1", TaxID = "taxId1_16498+544684", // 0
            Venues = new[]
            {
                new Venue
                {
                    Name = "The Creative Canvas", Address = "123 Art Street, New York, NY", MaxUsers = 25,
                    Price = 0, // 0
                    Location = new Location { Latitude = 40.7128, Longitude = -74.0060 },
                    Description = "Cozy art studio for painting lovers, with guided sessions and open studio time."
                },
                new Venue
                {
                    Name = "Peak Climb Center", Address = "456 Mountain Ave, Denver, CO", MaxUsers = 100,
                    Price = 49.99M, // 1
                    Location = new Location { Latitude = 39.7392, Longitude = -104.9903 },
                    Description =
                        "Indoor climbing gym with routes for all skill levels. Perfect for climbing enthusiasts."
                }
            }
        },
        new BusinessClient
        {
            Email = "bclient2@test.com", UserName = "BusinessClient2", TaxID = "taxId1_189481815+8787", // 1
            Venues = new[]
            {
                new Venue
                {
                    Name = "Gamers' Haven", Address = "789 Pixel Blvd, Austin, TX", MaxUsers = 40, Price = 20, // 0
                    Location = new Location { Latitude = 30.2672, Longitude = -97.7431 },
                    Description = "Space for tabletop games, RPGs, and board game meetups. Snacks and drinks available."
                },
                new Venue
                {
                    Name = "Strings & Things Music Hall", Address = "321 Melody Ln, Nashville, TN", MaxUsers = 60,
                    Price = 34.99M, // 1
                    Location = new Location { Latitude = 36.1627, Longitude = -86.7816 },
                    Description = "Jam, learn, or perform at this music spot. Instruments and sound gear provided."
                },
                new Venue
                {
                    Name = "Urban Garden Hub", Address = "159 Greenway Dr, Portland, OR", MaxUsers = 20, Price = 0, // 2
                    Location = new Location { Latitude = 45.5152, Longitude = -122.6784 },
                    Description = "Garden and workshop for enthusiasts. Tools, soil, and seasonal classes provided."
                }
            }
        }
    };

    private readonly List<Event> _events = new()
    {
        new Event
        {
            // 0
            Name = "Sunset Sketch Session",
            Description = "Evening drawing meetup in the park. Bring supplies and create nature-inspired art.",
            StartTime = DateTime.Now.AddDays(2).AddHours(17),
            EndTime = DateTime.Now.AddDays(2).AddHours(19),
            MinUsers = 2,
            MaxUsers = 15,
            Price = 9.99f,
            Location = new LocationNullable { Latitude = 34.0522, Longitude = -118.2437 },
            Venue = null
        },

        new Event
        {
            // 1
            Name = "Beachside Yoga Flow",
            Description = "Morning yoga session by the ocean to start your day fresh and relaxed. All levels welcome.",
            StartTime = DateTime.Now.AddDays(3).AddHours(7),
            EndTime = DateTime.Now.AddDays(3).AddHours(8),
            MinUsers = 1,
            MaxUsers = 20,
            Price = 11.99f,
            Location = new LocationNullable { Latitude = 36.9741, Longitude = -122.0308 },
            Venue = null
        },

        new Event
        {
            // 2
            Name = "Weekend Climb Challenge",
            Description =
                "Climbing competition for all skill levels. Prizes for top performers and refreshments included.",
            StartTime = DateTime.Now.AddDays(5).AddHours(10),
            EndTime = DateTime.Now.AddDays(5).AddHours(14),
            MinUsers = 10,
            MaxUsers = 50,
            Price = 25,
            Location = new LocationNullable { Latitude = null, Longitude = null }
        },

        new Event
        {
            // 3
            Name = "Indie Jam Night",
            Description =
                "Open mic night for local musicians and bands to perform live. BYO instrument or use house gear.",
            StartTime = DateTime.Now.AddDays(1).AddHours(19),
            EndTime = DateTime.Now.AddDays(1).AddHours(22),
            MinUsers = 5,
            MaxUsers = 60,
            Price = 14.99f,
            Location = new LocationNullable { Latitude = null, Longitude = null }
        },

        new Event
        {
            // 4
            Name = "Garden Prep & Seed Swap",
            Description =
                "Help prepare spring beds and exchange seeds with fellow gardeners. Tools and compost provided.",
            StartTime = DateTime.Now.AddDays(4).AddHours(10),
            EndTime = DateTime.Now.AddDays(4).AddHours(12),
            MinUsers = 3,
            MaxUsers = 20,
            Price = 5,
            Location = new LocationNullable { Latitude = null, Longitude = null }
        }
    };
    private readonly List<User> _users;

    private readonly List<Hobby> _hobbies = new()
    {
        new Hobby { Name = "Reading" },
        new Hobby { Name = "Gardening" },
        new Hobby { Name = "Painting" },
        new Hobby { Name = "Cycling" },
        new Hobby { Name = "Photography" },
        new Hobby { Name = "Cooking" },
        new Hobby { Name = "Hiking" },
        new Hobby { Name = "Swimming" },
        new Hobby { Name = "Writing" },
        new Hobby { Name = "Drawing" },
        new Hobby { Name = "Fishing" },
        new Hobby { Name = "Knitting" },
        new Hobby { Name = "Woodworking" },
        new Hobby { Name = "Yoga" },
        new Hobby { Name = "Playing Guitar" },
        new Hobby { Name = "Bird Watching" },
        new Hobby { Name = "Dancing" }
    };


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
        _events[0].SponsorsPartners = new List<BusinessClient> { _clients[0], _clients[1] };
        _events[1].SponsorsPartners = new List<BusinessClient> { _clients[1] };
        _events[2].SponsorsPartners = new List<BusinessClient> { _clients[0] };
        _events[4].SponsorsPartners = new List<BusinessClient> { _clients[1] };

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
                var userManager = context.GetService<UserManager<Organizer>>();

                foreach (var newClient in _clients)
                {
                    var bclient = context.Set<BusinessClient>().FirstOrDefault(u => u.Email == newClient.Email);
                    if (bclient == null) userManager.CreateAsync(newClient, "Pass123!").Wait();
                }

                foreach (var newUser in _users)
                {
                    var user = context.Set<User>().FirstOrDefault(u => u.Email == newUser.Email);
                    if (user == null) userManager.CreateAsync(newUser, "Pass123!").Wait();
                }

                foreach (var newHobby in _hobbies)
                {
                    var hobby = context.Set<Hobby>().FirstOrDefault(h => h.Name == newHobby.Name);
                    if (hobby == null) context.Set<Hobby>().Add(newHobby);
                }

                context.SaveChanges();
            })
            .UseAsyncSeeding(async (context, _, ct) =>
            {
                var userManager = context.GetService<UserManager<Organizer>>();

                foreach (var newClient in _clients)
                {
                    var bclient = await context.Set<BusinessClient>()
                        .FirstOrDefaultAsync(u => u.Email == newClient.Email);
                    if (bclient == null)
                    {
                        var res = await userManager.CreateAsync(newClient, "Pass123!");
                    }
                }

                foreach (var newUser in _users)
                {
                    var user = await context.Set<User>().FirstOrDefaultAsync(u => u.Email == newUser.Email);
                    if (user == null)
                    {
                        var res = await userManager.CreateAsync(newUser, "Pass123!");
                    }
                }

                foreach (var newHobby in _hobbies)
                {
                    var hobby = context.Set<Hobby>().FirstOrDefault(h => h.Name == newHobby.Name);
                    if (hobby == null)  await context.Set<Hobby>().AddAsync(newHobby);
                }

                await context.SaveChangesAsync();
            });
    }


    private List<User> generateUsers(int n)
    {
        var users = new List<User>();
        for (var i = 0; i < n; i++)
            users.Add(new User { Email = $"user{i + 1}@test.com", UserName = $"UserName{i + 1}" });

        return users;
    }
}