using System.ComponentModel.DataAnnotations;

namespace WebTraining.DB.Models
{
    public class TrainingExercise
    {
        [Key]
        public int ID { get; set; }

        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }

        public int TrainingId { get; set; }
        public Training? Training { get; set; }

        public int Repetitions { get; set; }

        public int Sets { get; set; }
    }
}
