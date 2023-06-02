namespace WebTraining.DB.Models.Measurements
{
    public class Waist
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public float Value { get; set; }

        public float Change { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
