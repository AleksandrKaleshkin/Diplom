using Microsoft.EntityFrameworkCore;
using WebTraining.DB.Models;

namespace WebTraining.DB.DataContext
{
    public class WebTrainingContext : DbContext
    {
        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Training> Training { get; set; }

        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        public DbSet<TypeOfMuscle> TypeOfMuscles { get; set; }

        public DbSet<Notepad> Notepads { get; set; }   



        public WebTrainingContext(DbContextOptions options) : base(options)        
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         modelBuilder.Entity<TypeOfMuscle>().HasData(
            new TypeOfMuscle { ID = 1, NameType = "Битцепс" },
            new TypeOfMuscle { ID = 2, NameType = "Пресс" },
            new TypeOfMuscle { ID = 3, NameType = "Трицепс" },
            new TypeOfMuscle { ID = 4, NameType = "Плечи" },
            new TypeOfMuscle { ID = 5, NameType = "Предплечья" },
            new TypeOfMuscle { ID = 6, NameType = "Голень" },
            new TypeOfMuscle { ID = 7, NameType = "Ноги" },
            new TypeOfMuscle { ID = 8, NameType = "Спина" },
            new TypeOfMuscle { ID = 9, NameType = "Грудь" }
    );


        }
    }
}
