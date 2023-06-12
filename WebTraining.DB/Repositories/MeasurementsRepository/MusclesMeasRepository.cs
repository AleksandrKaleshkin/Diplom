using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Repositories.MeasurementsRepository
{
    public class MusclesMeasRepository : IMusculesMeasRepository
    {
        WebTrainingContext db;

        public MusclesMeasRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public MusclesMeasurements GetMuscle(int id)
        {
            return db.MusclesMeasurements.Find(id);
        }

        public IEnumerable<MusclesMeasurements> AllMuscle()
        {
            return db.MusclesMeasurements.ToList();
        }
    }
}
