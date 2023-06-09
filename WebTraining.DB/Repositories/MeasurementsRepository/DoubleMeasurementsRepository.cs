using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Repositories.MeasurementsRepository
{
    public class DoubleMeasurementsRepository : IDoubleMeasRepository<DoubleMeasurements>
    {
        WebTrainingContext db;

        public DoubleMeasurementsRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(DoubleMeasurements item)
        {
            db.DoubleMeasurements.Add(item);
        }

        public void Delete(int id)
        {
            db.DoubleMeasurements.Remove(Get(id));
        }

        public DoubleMeasurements Get(int id)
        {
            DoubleMeasurements? meas = db.DoubleMeasurements.Find(id);
            if (meas != null)
            {
                return meas;
            }
            return null;
        }

        public IEnumerable<DoubleMeasurements> GetAll()
        {
            return db.DoubleMeasurements.Include(x=>x.User).Include(x=>x.TypeOfMuscle).ToList();
        }

        public MusclesMeasurements GetMuscles(int id)
        {
            return db.MusclesMeasurements.Find(id);
        }

        public IEnumerable<MusclesMeasurements> GetTypes()
        {
            return db.MusclesMeasurements.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(DoubleMeasurements item)
        {   
            db.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
