using HobbyMatch.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyMatch.Database.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users;
        public DbSet<BusinessClient> BusinessClients;
    }
}
