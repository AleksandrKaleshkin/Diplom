﻿using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Repositories.MeasurementsRepository
{
    public class SingleMeasurementsRepository : ISingleMeasRepository<SingleMeasurements>
    {
        WebTrainingContext db;

        public SingleMeasurementsRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(SingleMeasurements item)
        {
            db.SingleMeasurements.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            db.SingleMeasurements.Remove(Get(id));
            Save();
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

        public void Update(SingleMeasurements item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
