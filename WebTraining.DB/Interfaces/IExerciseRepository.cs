using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface IExerciseRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        TypeOfMuscle GetType(int id);
        IEnumerable<TypeOfMuscle> GetTypes();
        void AddPicture(ImageExercise image);
        IEnumerable<ImageExercise> GetImage(Exercise exercise);
        IEnumerable<ImageExercise> GetImages();
        ImageExercise GetNeedImage(int id);
        void UpdatePic(ImageExercise item);
        void DeleteImage(int id);
        void Save();
    }
}
