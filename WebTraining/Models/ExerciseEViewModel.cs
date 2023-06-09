using WebTraining.Core.DTO;

namespace WebTraining.Models
{
    public class ExerciseEViewModel
    {
        public ExerciseDTO Exercise { get; set; }
        public IEnumerable<ImageExerciseDTO> Image { get; set; }
    }
}
