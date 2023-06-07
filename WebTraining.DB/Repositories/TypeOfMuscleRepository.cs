using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class TypeOfMuscleRepository: ITypeRepository<TypeOfMuscle>
    {
        private WebTrainingContext db;

        public TypeOfMuscleRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public TypeOfMuscle Get(int id)
        {
            return db.TypeOfMuscles.Find(id);
        }

        public IEnumerable<TypeOfMuscle> GetTypes()
        {
            return db.TypeOfMuscles.ToList();
        }


    }
}
