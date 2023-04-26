using WebTraining.Core.DTO;

namespace WebTraining.Core.Interfaces
{
    public interface IExerciseService
    {
        ExerciseDTO GetExercise(int id);
        void DeleteExercise(int id);
        void AddExercise(ExerciseDTO exercise);
        IEnumerable<ExerciseDTO> GetExercises();
        void UpdateExercise(ExerciseDTO exercise);
        void Dispose();
    }
}
