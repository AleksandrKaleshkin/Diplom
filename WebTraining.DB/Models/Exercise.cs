using System.ComponentModel.DataAnnotations;

namespace WebTraining.DB.Models
{
    public class Exercise
    {
        [Key]
        public int ID { get; set; }

        public string? NameExercise { get; set; }

        public string? TypeOfMuscleID { get; set; }

        public string? Description { get; set; }

        public string? NameImage1 { get; set; }
        public string? PathImage1 { get; set; }

        public string? NameImage2 { get; set; }
        public string? PathImage2 { get; set; }

        public string? NameImage3 { get; set; }
        public string? PathImage3 { get; set; }

        public List<Training> Trainings { get; set; } = new();

        public List<TrainingExercise> TrainingExercise { get; set; } = new();


    }
}
