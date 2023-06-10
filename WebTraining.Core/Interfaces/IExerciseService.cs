using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Interfaces
{
    public interface IExerciseService
    {
        ExerciseDTO GetExercise(int id);
        void DeleteExercise(int id);
        Exercise AddExercise(ExerciseDTO exercise);
        IEnumerable<ExerciseDTO> GetExercises();
        void UpdateExercise(ExerciseDTO exercise);
        void DeleteImage(int id);
        void UpdatePicture(ImageExerciseDTO image);
        void AddPicture(ImageExerciseDTO image);
        IEnumerable<ImageExerciseDTO> GetImageExercises(ExerciseDTO exercise);
        IEnumerable<ImageExerciseDTO> GetImages();

        IEnumerable<TypeOfMuscleDTO> GetTypeOfMuscles();
    }
}
