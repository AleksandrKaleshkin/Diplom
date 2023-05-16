using WebTraining.Core.DTO;

namespace WebTraining.Core.Interfaces
{
    public interface ITrainingExerciseService
    {
        IEnumerable<TrainingExerciseDTO> GetExercises();
        void AddExercise(TrainingExerciseDTO exercise);
        TrainingExerciseDTO GetExercise(int id);
        TrainingExerciseDTO GetNeedExercise(int id);
        void UpdateExercise(TrainingExerciseDTO exercise);
        List<TrainingExerciseDTO> GetNeedExercises(int id);
        void DeleteExercise(int id);
        void Dispose();
    }
}
