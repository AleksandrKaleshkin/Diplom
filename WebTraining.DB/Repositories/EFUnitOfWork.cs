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
        private TypeOfMuscleRepository typeOfMuscleRepository;
        private TrainingExerciseRepository trainingExerciseRepository;
        private NotepadRepository notepadRepository;

        public EFUnitOfWork(WebTrainingContext db)
        {
            this.db = db;
        }


        IRepository<TrainingExercise> IUnitOfWork.TrainingExercise
        {
            get
            {
                if (trainingExerciseRepository == null)
                {
                    trainingExerciseRepository = new TrainingExerciseRepository(db);
                }
                return trainingExerciseRepository;
            }
        }


        IRepository<Exercise> IUnitOfWork.Exercises
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

        IRepository<Training> IUnitOfWork.Training
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

        ITypeRepository<TypeOfMuscle> IUnitOfWork.Type
        {
            get
            {
                if (typeOfMuscleRepository == null)
                {
                    typeOfMuscleRepository = new TypeOfMuscleRepository(db);
                }
                return typeOfMuscleRepository;
            }
        }

        IRepository<Notepad> IUnitOfWork.Notepads
        {
            get
            {
                if (notepadRepository == null)
                {
                    notepadRepository = new NotepadRepository(db);
                }
                return notepadRepository;
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
