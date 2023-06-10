using WebTraining.Core.DTO;
using WebTraining.Core.Models;

namespace WebTraining.Models
{
    public class AddEditExerciseViewModel
    {
        public ExerciseDTO ExerciseDTO { get; set; }
        public TypeOfMyscList? TypeOfMysc { get; set; }
        public IEnumerable<ImageExerciseDTO>? ImageExercise { get; set; }
    }
}
