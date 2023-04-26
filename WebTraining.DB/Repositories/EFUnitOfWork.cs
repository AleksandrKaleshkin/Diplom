using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.DB.Repositories
{
    public class EFUnitOfWork:IUnitOfWork   
    {
        private WebTrainingContext db;
        private TrainingRepository trainingRepository;
        private ExerciseRepository exerciseRepository;

        public EFUnitOfWork(WebTrainingContext db)
        {
            this.db = db;
        }





        IRepository<Exercise> IUnitOfWork.ExerciseRepository
        {
            get
            {
                if (exerciseRepository == null)
                {
                    exerciseRepository = new ExerciseRepository(db);
                }
                return exerciseRepository;
            }
        }

        IRepository<Training> IUnitOfWork.TrainingRepository
        {
            get
            {
                if (trainingRepository == null)
                {
                    trainingRepository = new TrainingRepository(db);
                }
                return trainingRepository;
            }
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
                Dispose(true);
                GC.SuppressFinalize(this);
        }


        public void Save()
        {
            db.SaveChanges();
        }
    }
}
