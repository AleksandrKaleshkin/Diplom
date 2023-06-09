using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Interfaces.IMeasurements
{
    public interface IDoubleMeasurementsService
    {
        DoubleMeasurementsDTO GetMeasurement(int id);
        void DeleteMeasurement(int id);
        void AddMeasurement(DoubleMeasurementsDTO measDTO, User user);
        IEnumerable<DoubleMeasurementsDTO> GetMeasurements();
        void UpdateMeasurement(DoubleMeasurementsDTO measDTO, User user);
        IEnumerable<DoubleMeasurementsDTO> GetNeedMeasurements(User user, int type);
        MusclesMeasurementsDTO GetTypeOfMuscle(string type);
    }
}
