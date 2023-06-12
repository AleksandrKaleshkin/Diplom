using WebTraining.DB.Models;

namespace WebTraining.DB.Interfaces
{
    public interface ITypeOfMuscleRepository
    {
        TypeOfMuscle GetType(int id);
        IEnumerable<TypeOfMuscle> GetAllTypes();
    }
}
