using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TrainScheduler.Model.Entities;
using TrainScheduler.Model.Enums;

namespace TrainScheduler.Core.Database
{
    public class TrainSchedulerContext : IdentityDbContext<IdentityUser>
    {
        public TrainSchedulerContext(DbContextOptions<TrainSchedulerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedData(builder);

            base.OnModelCreating(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            var userRole = new IdentityRole(RoleNames.User.ToString()) { NormalizedName = RoleNames.User.ToString().ToUpper() };
            var adminRole = new IdentityRole(RoleNames.Admin.ToString()) { NormalizedName = RoleNames.Admin.ToString().ToUpper() };

            builder.Entity<IdentityRole>().HasData(userRole, adminRole);

            var hasher = new PasswordHasher<IdentityUser>();
            var adminEmail = "admin@mail.com";

            var admin = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = adminEmail,
                UserName = adminEmail,
                NormalizedEmail = adminEmail,
                NormalizedUserName = adminEmail,
                PasswordHash = hasher.HashPassword(null, "admin"),
                SecurityStamp = string.Empty
            };

            builder.Entity<IdentityUser>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = admin.Id
            });
        }
    }
}
