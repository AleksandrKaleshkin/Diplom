using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface ITERepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<Exercise> GetAllExercise();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();

    }
}
