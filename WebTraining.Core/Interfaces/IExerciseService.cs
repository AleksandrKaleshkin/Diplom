using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Interfaces
{
    public interface IExerciseService
    {
        ExerciseDTO GetExercise(int id);
        void DeleteExercise(int id);
        void AddExercise(ExerciseDTO exercise, int type);
        IEnumerable<ExerciseDTO> GetExercises();
        void UpdateExercise(ExerciseDTO exercise, int type);
        IEnumerable<TypeOfMuscleDTO> GetTypeOfMuscles();
        void Dispose();
    }
}
