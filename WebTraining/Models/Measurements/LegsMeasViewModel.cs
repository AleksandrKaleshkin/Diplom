using WebTraining.Core.DTO.MeasurementsDTO;

namespace WebTraining.Models.Measurements
{
    public class LegsMeasViewModel
    {
        public IEnumerable<DoubleMeasurementsDTO>? Measurements { get; set; }

        public string Muscles { get; set; } = "Ноги";
    }
}
