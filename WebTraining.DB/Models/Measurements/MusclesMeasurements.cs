namespace WebTraining.DB.Models.Measurements
{
    public class MusclesMeasurements
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<DoubleMeasurements> DoubleMeasurements { get; set; } = new();

        public List<SingleMeasurements> SingleMeasurements { get; set; } = new();
    }
}
