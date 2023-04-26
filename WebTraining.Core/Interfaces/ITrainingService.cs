using WebTraining.Core.DTO;

namespace WebTraining.Core.Interfaces
{
    public interface ITrainingService
    {
        TrainingDTO GetTraining(int id);
        void DeleteTraining(int id);
        void AddTraing(TrainingDTO training);
        IEnumerable<TrainingDTO> GetTraining();
        void UpdateTraining(TrainingDTO training);
        void Dispose();
    }
}
