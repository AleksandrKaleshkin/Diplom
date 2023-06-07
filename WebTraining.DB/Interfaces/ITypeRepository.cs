using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface ITypeRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetTypes();
    }
}
