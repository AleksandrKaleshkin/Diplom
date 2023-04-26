using Microsoft.EntityFrameworkCore;
using WebTraining.DB.Models;

namespace WebTraining.DB.DataContext
{
    public class WebTrainingContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Training> Training { get; set; }

        public DbSet<TrainingExercise> TrainingExercises { get; set; }


        public WebTrainingContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }


    }
}
