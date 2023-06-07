using WebTraining.Core.DTO.MeasurementsDTO;

namespace WebTraining.Models.Measurements
{
    public class BreastMeasViewModel
    {
        public IEnumerable<SingleMeasurementstDTO>? Measurements { get; set; }

        public string Muscles { get; set; } = "Грудь";
    }
}
