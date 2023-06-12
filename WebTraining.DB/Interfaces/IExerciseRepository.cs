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
        void Save();
    }
}
