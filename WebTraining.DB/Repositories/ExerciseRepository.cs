using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.InitializeData;

namespace WebTraining.DB.Repositories
{
    public class ExerciseRepository : IExerciseRepository<Exercise>
    {
        private WebTrainingContext db;

        public ExerciseRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(Exercise item)
        {
            db.Exercises.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            Exercise? exercise = db.Exercises.Find(id);
            if (exercise != null)
            {
                db.Exercises.Remove(exercise);
                Save();
            }
        }

        public Exercise Get(int id)
        {
            var exercise = db.Exercises.Include(o => o.TypeOfMuscle).FirstOrDefault(x=>x.ID==id);
            if (exercise !=null)
            {
                return exercise;
            }
            return null;
        }

        public IEnumerable<Exercise> GetAll()
        {
         
            if (db.Exercises.Count()==0)
            {
                ExerciseInitializer initializer = new ExerciseInitializer(db);
                initializer.InitializeExercise();
                db.SaveChanges();
            }
            return db.Exercises.Include(o=>o.TypeOfMuscle);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Exercise item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
