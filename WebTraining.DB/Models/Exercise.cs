using System.ComponentModel.DataAnnotations;

namespace WebTraining.DB.Models
{
    public class Exercise
    {
        [Key]
        public int ID { get; set; }

        public string? NameExercise { get; set; }

        public int TypeOfMuscleID { get; set; }
        public TypeOfMuscle? TypeOfMuscle { get; set; }

        public string? Description { get; set; }

        public List<ImageExercise> Image { get; set; } = new();

        public List<Training> Trainings { get; set; } = new();

        public List<TrainingExercise> TrainingExercise { get; set; } = new();
    }
}
