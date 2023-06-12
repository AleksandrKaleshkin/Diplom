using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface ITrainingRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

        User GetUser(string id);
        IEnumerable<User> GetAllUsers();

        void Save();
    }
}
