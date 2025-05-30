﻿using HobbyMatch.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HobbyMatch.Database.Data
{
    public class AppDbContext : IdentityDbContext<Organizer, IdentityRole<int>, int>
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> AppUsers { get; set; }
        public DbSet<BusinessClient> BusinessClients { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            new DbSeeder().SetUpDbSeeding(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity(
                "BusinessClientEvent",
                b =>
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
                }
            );

            modelBuilder.Entity(
                "EventUser",
                b =>
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
                }
            );

            modelBuilder.Entity<UserFriendship>(b =>
            {
                b.HasKey(uf => new { uf.UserId, uf.FriendId });
                b.HasOne(uf => uf.User)
                    .WithMany()
                    .HasForeignKey(uf => uf.UserId)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(uf => uf.Friend)
                    .WithMany()
                    .HasForeignKey(uf => uf.FriendId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity(
                "EventHobby",
                b =>
                {
                    b.HasOne("HobbyMatch.Domain.Entities.Hobby", null)
                        .WithMany()
                        .HasForeignKey("HobbiesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("HobbyMatch.Domain.Entities.Event", null)
                        .WithMany()
                        .HasForeignKey("HobbiesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                }
            );

            modelBuilder.Entity(
                "HobbyUser",
                b =>
                {
                    b.HasOne("HobbyMatch.Domain.Entities.Hobby", null)
                        .WithMany()
                        .HasForeignKey("HobbiesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                    b.HasOne("HobbyMatch.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("HobbiesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                }
            );

            modelBuilder
                .Entity<Comment>()
                .HasOne(c => c.Organizer)
                .WithMany(o => o.Comments)
                .HasForeignKey(c => c.OrganizerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Comment>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
