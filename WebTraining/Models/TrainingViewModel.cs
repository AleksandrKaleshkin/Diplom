using WebTraining.Core.DTO;

namespace WebTraining.Models
{
    public class TrainingViewModel
    {
        public IEnumerable<TrainingDTO>? Trainings { get; set; }
        public IEnumerable<TrainingExerciseDTO>? ExerciseTraining { get; set; }
    }
}
