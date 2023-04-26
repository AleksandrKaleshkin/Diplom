using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class ExerciseRepository : IRepository<Exercise>
    {
        private WebTrainingContext db;

        public ExerciseRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(Exercise item)
        {
            db.Exercises.Add(item);
        }

        public void Delete(int id)
        {
            Exercise? exercise = db.Exercises.Find(id);
            if (exercise != null)
            {
                db.Exercises.Remove(exercise);
            }
        }

        public Exercise Get(int id)
        {
            var exercise = db.Exercises.Find(id);
            if (exercise !=null)
            {
                return exercise;
            }
            return null;
        }

        public IEnumerable<Exercise> GetAll()
        {
            return db.Exercises.ToList();
        }

        public void Update(Exercise item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
