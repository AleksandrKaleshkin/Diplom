using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class TrainingExerciseRepository : IRepository<TrainingExercise>
    {
        private WebTrainingContext db;

        public TrainingExerciseRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(TrainingExercise item)
        {
            db.TrainingExercises.Add(item);
        }

        public void Delete(int id)
        {
            TrainingExercise? trainExse = db.TrainingExercises.Find(id);
            if (trainExse != null)
            {
                db.TrainingExercises.Remove(trainExse);
            }
        }

        public TrainingExercise Get(int id)
        {
            var trainExse = db.TrainingExercises.Find(id);
            if (trainExse !=null)
            {
                return trainExse;
            }
            return null;
        }

        public IEnumerable<TrainingExercise> GetAll()
        {
            return db.TrainingExercises.Include(o=>o.Exercise).ToList();
        }

        public List<TrainingExercise> GetExercises(int id)
        {
            IEnumerable<TrainingExercise> training = GetAll();
            List<TrainingExercise> exercises = new List<TrainingExercise>();
            foreach (var item in training)
            {
                if (item.TrainingId==id)
                {
                    exercises.Add(item);
                }
            }
            return exercises;
        }

        public void Update(TrainingExercise item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
