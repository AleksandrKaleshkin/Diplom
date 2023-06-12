using Microsoft.EntityFrameworkCore;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class NotepadRepository : INotepadRepository<Notepad>
    {
        private WebTrainingContext db;

        public NotepadRepository(WebTrainingContext db)
        {
            this.db = db;
        }

        public void Create(Notepad item)
        {
            db.Notepads.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            Notepad? note = db.Notepads.Find(id);
            if (note != null)
            {
                db.Notepads.Remove(note);
                Save();
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

        public async Task<IEnumerable<Notepad>> GetAll()
        {
            return await db.Notepads.ToListAsync();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Notepad item)
        {
            db.Entry(item).State = EntityState.Modified;
            Save();

        }
    }
}
