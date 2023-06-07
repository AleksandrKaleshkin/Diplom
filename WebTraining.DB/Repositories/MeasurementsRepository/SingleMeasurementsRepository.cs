using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Repositories.MeasurementsRepository
{
    public class SingleMeasurementsRepository : IRepository<SingleMeasurements>
    {
        WebTrainingContext db;

        public SingleMeasurementsRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(SingleMeasurements item)
        {
            db.SingleMeasurements.Add(item);
        }

        public void Delete(int id)
        {
            db.SingleMeasurements.Remove(Get(id));
        }

        public SingleMeasurements Get(int id)
        {
            SingleMeasurements meas = db.SingleMeasurements.Find(id);
            if (meas != null)
            {
                return meas;
            }
            return null;
        }

        public IEnumerable<SingleMeasurements> GetAll()
        {
            return db.SingleMeasurements.Include(x=>x.User).ToList();
        }

        public void Update(SingleMeasurements item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
