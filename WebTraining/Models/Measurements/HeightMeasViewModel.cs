using WebTraining.Core.DTO.MeasurementsDTO;

namespace WebTraining.Models.Measurements
{
    public class HeightMeasViewModel
    {
        public IEnumerable<SingleMeasurementstDTO>? Measurements { get; set; }

        public string Muscles { get; set; } = "Рост";
    }
}
