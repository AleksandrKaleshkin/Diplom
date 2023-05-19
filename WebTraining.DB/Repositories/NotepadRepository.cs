using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class NotepadRepository : IRepository<Notepad>
    {
        private WebTrainingContext db;

        public NotepadRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(Notepad item)
        {
            db.Notepads.Add(item);
        }

        public void Delete(int id)
        {
            Notepad? note = db.Notepads.Find(id);
            if (note != null)
            {
                db.Notepads.Remove(note);
            }
        }

        public Notepad Get(int id)
        {
            var note = db.Notepads.Find(id);
            if (note != null)
            {
                return note;
            }
            return null;
        }

        public IEnumerable<Notepad> GetAll()
        {
            return db.Notepads.ToList();
        }

        public void Update(Notepad item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
    }
}
