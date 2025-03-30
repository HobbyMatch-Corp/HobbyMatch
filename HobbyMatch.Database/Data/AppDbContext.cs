using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Data
{
    public class AppDbContext : IdentityDbContext<Organizer,IdentityRole<int>,int>
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
        }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<BusinessClient> BusinessClients { get; set; }
        
    }
}
