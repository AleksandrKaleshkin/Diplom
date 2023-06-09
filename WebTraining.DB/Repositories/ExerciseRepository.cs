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
            var exercise = db.Exercises.Find(id);
            if (exercise !=null)
            {
                return exercise;
            }
            return null;
        }

        public IEnumerable<Exercise> GetAll()
        {
         
            if (db.Exercises.AsNoTracking().Count()==0)
            {
                ExerciseInitializer initializer = new ExerciseInitializer(db);
                initializer.InitializeExercise();
                db.SaveChanges();
            }
            return db.Exercises.AsNoTracking().Include(o=>o.TypeOfMuscle);
        }
        public IEnumerable<ImageExercise> GetImage(Exercise exercise)
        {
            return db.ImageExercises.ToList().Where(x => x.Exercise == exercise);
        }

        public ImageExercise GetImage(int id)
        {
            var image = db.ImageExercises.Find(id);
            if (image != null)
            {
                return image;
            }
            return null;
        }

        public void DeleteImage(int id)
        {
            db.ImageExercises.Remove(GetImage(id));
        }

        public IEnumerable<ImageExercise> GetImages()
        {
            if (db.ImageExercises.AsNoTracking().Count() == 0)
            {
                ExerciseInitializer initializer = new ExerciseInitializer(db);
                initializer.InitializeImage();
                db.SaveChanges();
            }
            return db.ImageExercises.AsNoTracking().ToList();
        }

        public void AddPicture(ImageExercise image)
        {
            db.ImageExercises.Add(image);
            Save();
        }

        public TypeOfMuscle GetType(int id)
        {
            return db.TypeOfMuscles.Find(id);
        }

        public IEnumerable<TypeOfMuscle> GetTypes()
        {
            return db.TypeOfMuscles.ToList();
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
