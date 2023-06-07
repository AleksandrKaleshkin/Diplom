namespace WebTraining.DB.Models.Measurements
{
    public class DoubleMeasurements
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }
        
        public int MuscleId { get; set; }

        public MusclesMeasurements? TypeOfMuscle { get; set; }

        public float LeftValue { get; set; }

        public float RightValue { get; set; }

        public float LeftChange { get; set; }

        public float RightChange { get; set; }

        public string UserId { get; set; }

        public User? User { get; set; }
    }
}
