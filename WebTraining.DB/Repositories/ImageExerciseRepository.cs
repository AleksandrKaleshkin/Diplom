using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.InitializeData;

namespace WebTraining.DB.Repositories
{
    public class ImageExerciseRepository : IimageExerciseRepository
    {
        private WebTrainingContext db;

        public ImageExerciseRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void UpdatePic(ImageExercise item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
        }

        public IEnumerable<ImageExercise> GetExerciseImage(Exercise exercise)
        {
            return db.ImageExercises.ToList().Where(x => x.Exercise == exercise);
        }

        public ImageExercise GetExerciseImage(int id)
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
            db.ImageExercises.Remove(GetExerciseImage(id));
            Save();
        }

        public IEnumerable<ImageExercise> GetImages()
        {
            if (db.ImageExercises.Count() == 0)
            {
                ExerciseInitializer initializer = new ExerciseInitializer(db);
                initializer.InitializeImage();
                db.SaveChanges();
            }
            return db.ImageExercises.ToList();
        }

        public void AddPicture(ImageExercise image)
        {
            db.ImageExercises.Add(image);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
