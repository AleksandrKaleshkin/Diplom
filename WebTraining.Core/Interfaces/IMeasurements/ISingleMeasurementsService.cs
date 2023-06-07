using WebTraining.Core.DTO;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Interfaces.IMeasurements
{
    public interface ISingleMeasurementstService
    {
        SingleMeasurementstDTO GetMeasurement(int id);
        void DeleteMeasurement(int id);
        void AddMeasurement(SingleMeasurementstDTO measDTO, User user);
        IEnumerable<SingleMeasurementstDTO> GetMeasurements();
        void UpdateMeasurement(SingleMeasurementstDTO measDTO, User user);
        IEnumerable<SingleMeasurementstDTO> GetNeedMeasurements(User user, int type);
        MusclesMeasurementsDTO GetTypeOfMuscle(string type);
        void Dispose();
    }
}
