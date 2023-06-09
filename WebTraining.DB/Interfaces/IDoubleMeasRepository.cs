using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Interfaces
{
    public interface IDoubleMeasRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        IEnumerable<MusclesMeasurements> GetTypes();
        MusclesMeasurements GetMuscles(int id);
        void Save();
    }
}
