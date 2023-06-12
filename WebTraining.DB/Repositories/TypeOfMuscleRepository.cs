using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class TypeOfMuscleRepository : ITypeOfMuscleRepository
    {
        private WebTrainingContext db;

        public TypeOfMuscleRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public IEnumerable<TypeOfMuscle> GetAllTypes()
        {
            return db.TypeOfMuscles.ToList();
        }

        public TypeOfMuscle GetType(int id)
        {
            return db.TypeOfMuscles.Find(id);
        }
    }
}
