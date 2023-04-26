using System.ComponentModel.DataAnnotations;
using System.Data;

namespace WebTraining.DB.Models
{
    public class Training
    {
        [Key]
        public int ID { get; set; }

        public string? NameTraining { get; set; }

        public DateTime DateTraining { get; set; }

        public List<Exercise> Exercisies { get; set; } = new();

        public List<TrainingExercise> TrainingExercisies { get; set; } = new();


    }
}
