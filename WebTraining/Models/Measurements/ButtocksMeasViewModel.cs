using WebTraining.Core.DTO.MeasurementsDTO;

namespace WebTraining.Models.Measurements
{
    public class ButtocksMeasViewModel
    {
        public IEnumerable<SingleMeasurementstDTO>? Measurements { get; set; }

        public string Muscles { get; set; } = "Ягодицы";
    }
}
