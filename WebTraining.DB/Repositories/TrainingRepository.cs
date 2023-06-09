using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class TrainingRepository : ITrainingRepository<Training>
    {
        private WebTrainingContext db;

        public TrainingRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(Training item)
        {
            db.Training.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            Training training = db.Training.Find(id);
            if (training != null)
            {
                db.Training.Remove(training);
                Save();
            }
        }

        public Training Get(int id)
        {
            return db.Training.Find(id);
        }

        public IEnumerable<Training> GetAll()
        {
            return db.Training.Include(o=>o.User);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        public User GetUser(string id)
        {
            return db.Users.Find(id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Training item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
