using IoTSensorPortal.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IoTSensorPortal.Infrastructure.Data.Contexts
{
    /// <summary>
    /// Entity framework context for IoTSensorPortalDb
    /// </summary>
    public class IoTSensorPortalContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Table with Sensors
        /// </summary>
        public DbSet<Sensor> Sensors { get; set; }

        /// <summary>
        /// Table with History
        /// </summary>
        public DbSet<History> History { get; set; }

        public IoTSensorPortalContext(DbContextOptions<IoTSensorPortalContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// Database description - tables names, foreign keys etc.
        /// It can be substituted by Data anotacion attributes in models
        /// </summary>
        /// <param name="modelBuilder">Entity framework modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sensor>().ToTable("Sensors");
            modelBuilder.Entity<History>().ToTable("History");

            modelBuilder.Entity<UserSensor>()
                .HasKey(us => new { us.ApplicationUserId, us.SensorId });

            modelBuilder.Entity<UserSensor>()
                .HasOne(us => us.ApplicationUser)
                .WithMany(u => u.SharedSensors)
                .HasForeignKey(us => us.ApplicationUserId);

            modelBuilder.Entity<UserSensor>()
                .HasOne(us => us.Sensor)
                .WithMany(s => s.SharedWithUsers)
                .HasForeignKey(us => us.SensorId);
        }
    }
}
