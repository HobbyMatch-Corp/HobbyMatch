using HobbyMatch.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BusinessClient> BusinessClients { get; set; }
    }
}
