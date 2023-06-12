using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTraining.DB.Models
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string NameExercise { get; set; }

        public int TypeOfMuscleID { get; set; }
        public TypeOfMuscle TypeOfMuscle { get; set; }

        public string Description { get; set; }

        public List<ImageExercise> Image { get; set; } = new();

        public List<Training> Trainings { get; set; } = new();

        public List<TrainingExercise> TrainingExercise { get; set; } = new();
    }
}
