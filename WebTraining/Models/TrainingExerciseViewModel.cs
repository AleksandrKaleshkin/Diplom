using WebTraining.Core.DTO;

namespace WebTraining.Models
{
    public class TrainingExerciseViewModel
    {
        public List<TrainingExerciseDTO>? ExerciseTraining { get; set; }

        public int? TrainingId { get;set; }
    }
}
