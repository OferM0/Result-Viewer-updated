using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ResultViewer.Server.Context
{
    // The DbContext class that represents the database context.
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions to configure the context.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Set a command timeout for database operations.
            //if any database operation takes longer than 180 seconds to complete, it might be canceled.
            this.Database.SetCommandTimeout(TimeSpan.FromSeconds(180));
        }

        // Define DbSet properties for each entity that represents a table in the database.
        public DbSet<LotRun> LotRuns { get; set; }
        public DbSet<WaferRun> WaferRuns { get; set; }
        public DbSet<RecipeRun> RecipeRuns { get; set; }
        public DbSet<TestRecipeRun> TestRecipeRuns { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<AROLMeasurement> AROLMeasurements { get; set; }
        public DbSet<AROLAccuracy> AROLAccuracies { get; set; }

        // used to configure the relationships between entities 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between LotRun and WaferRun entities.
            modelBuilder
                .Entity<LotRun>()
                .HasMany(p => p.WaferRuns)
                .WithOne(p => p.LotRun)
                .HasForeignKey(p => p.Wafer_Run_Id);

            // Configure the relationship between RecipeRun and LotRun entities.
            modelBuilder
                .Entity<RecipeRun>()
                .HasMany(p => p.LotRuns)
                .WithOne(p => p.RecipeRun)
                .HasForeignKey(p => p.Run_Id);

            // Configure the relationship between RecipeRun and TestRecipeRun entities.
            modelBuilder
                .Entity<RecipeRun>()
                .HasMany(p => p.TestRecipeRuns)
                .WithOne(p => p.RecipeRun)
                .HasForeignKey(p => p.Recipe_Run_Id);

            // Configure the relationship between WaferRun and LotRun entities.
            modelBuilder
                .Entity<WaferRun>()
                .HasOne(p => p.LotRun)
                .WithMany(p => p.WaferRuns)
                .HasForeignKey(p => p.Run_Id);

            // Configure the relationship between LotRun and RecipeRun entities.
            modelBuilder
                .Entity<LotRun>()
                .HasOne(p => p.RecipeRun)
                .WithMany(p => p.LotRuns)
                .HasForeignKey(p => p.Recipe_Run_Id);

            // Configure the relationship between LotRun and Measurement entities.
            modelBuilder
                .Entity<LotRun>()
                .HasMany(p => p.Measurements)
                .WithOne(p => p.LotRun)
                .HasForeignKey(p => p.Run_Id);

            // Configure the relationship between Measurement and LotRun entities.
            modelBuilder
                .Entity<Measurement>()
                .HasOne(p => p.LotRun)
                .WithMany(p => p.Measurements)
                .HasForeignKey(p => p.Measurement_Id);

            // Configure the relationship between Measurement and AROLMeasurement entities.
            modelBuilder
                .Entity<Measurement>()
                .HasOne(p => p.AROLMeasurement)
                .WithOne(p => p.Measurement)
                .HasForeignKey<AROLMeasurement>(p => p.Measurement_Id);

            // Configure the relationship between Measurement and AROLAccuracy entities.
            modelBuilder
                .Entity<Measurement>()
                .HasOne(p => p.AROLAccuracy)
                .WithOne(p => p.Measurement)
                .HasForeignKey<AROLAccuracy>(p => p.Measurement_Id);
        }
    }
}

