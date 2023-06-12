using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface IimageExerciseRepository
    {
        void AddPicture(ImageExercise image);
        IEnumerable<ImageExercise> GetExerciseImage(Exercise exercise);
        IEnumerable<ImageExercise> GetImages();
        ImageExercise GetExerciseImage(int id);
        void UpdatePic(ImageExercise item);
        void DeleteImage(int id);
        void Save();
    }
}
