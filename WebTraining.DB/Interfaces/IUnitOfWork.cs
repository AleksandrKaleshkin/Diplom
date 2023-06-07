using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Exercise> Exercises { get; }
        IRepository<Training> Training { get; }
        ITypeRepository<TypeOfMuscle> Type { get; }
        IRepository<TrainingExercise> TrainingExercise { get; } 
        IRepository<Notepad> Notepads { get; }
        IRepository<DoubleMeasurements> DoubleMeasurements { get; }
        IRepository<SingleMeasurements> SingleMeasurements { get; }
        ITypeRepository<MusclesMeasurements> Muscles { get; }

        void Save();
    }
}
