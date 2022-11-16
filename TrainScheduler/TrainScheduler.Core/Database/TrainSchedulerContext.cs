using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainScheduler.Model.Entities;

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
    }
}
