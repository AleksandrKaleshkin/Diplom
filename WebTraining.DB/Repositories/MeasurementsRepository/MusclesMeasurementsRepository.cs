using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Repositories.MeasurementsRepository
{
    public class MusclesMeasurementsRepository : ITypeRepository<MusclesMeasurements>
    {
        private WebTrainingContext db;

        public MusclesMeasurementsRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public MusclesMeasurements Get(int id)
        {
            return db.MusclesMeasurements.Find(id);
        }


        public IEnumerable<MusclesMeasurements> GetTypes()
        {
            return db.MusclesMeasurements.ToList();
        }
    }
}
