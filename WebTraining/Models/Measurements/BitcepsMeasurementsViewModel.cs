using WebTraining.Core.DTO.MeasurementsDTO;

namespace WebTraining.Models.Measurements
{
    public class BitcepsMeasurementsViewModel
    {
        public IEnumerable<DoubleMeasurementsDTO>? Measurements { get; set; }

        public string Muscles { get; set; } = "Битцепс";
    }
}
