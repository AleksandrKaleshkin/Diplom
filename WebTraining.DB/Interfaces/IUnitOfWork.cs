using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Exercise> Exercises { get; }
        IRepository<Training> Training { get; }
        void Save();
    }
}
