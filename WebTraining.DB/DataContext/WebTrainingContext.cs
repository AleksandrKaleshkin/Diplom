using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.DataContext
{
    public class WebTrainingContext : IdentityDbContext<User>
    {
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Training> Training { get; set; }

        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        public DbSet<TypeOfMuscle> TypeOfMuscles { get; set; }

        public DbSet<MusclesMeasurements> MusclesMeasurements { get; set; }

        public DbSet<Notepad> Notepads { get; set; }   

        public DbSet<DoubleMeasurements> DoubleMeasurements { get; set; }

        public DbSet<SingleMeasurements> SingleMeasurements { get; set; }

        public DbSet<ImageExercise> ImageExercises { get; set; }




        public WebTrainingContext(DbContextOptions options) : base(options)
        { 
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            WebTrainingInitializer initializer = new WebTrainingInitializer();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TypeOfMuscle>().HasData(initializer.InitilizerType());
            modelBuilder.Entity<MusclesMeasurements>().HasData(initializer.InitilizerMusclesMeasurements());

        }
        
    }
}
