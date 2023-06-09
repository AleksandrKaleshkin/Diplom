using WebTraining.Core.DTO;

namespace WebTraining.Core.Interfaces
{
    public interface ITrainingExerciseService
    {
        IEnumerable<TrainingExerciseDTO> GetExercises();
        void AddExercise(TrainingExerciseDTO model);
        TrainingExerciseDTO GetExercise(int id);
        TrainingExerciseDTO GetNeedExercise(int id);
        void UpdateExercise(TrainingExerciseDTO exercise);
        List<TrainingExerciseDTO> GetNeedExercises(int id);
        IEnumerable<ExerciseDTO> GetExerciseList();
        void DeleteExercise(int id);
    }
}
