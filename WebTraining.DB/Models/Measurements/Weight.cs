namespace WebTraining.DB.Models.Measurements
{
    public class Weight
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public float ValueWeight { get; set; }

        public float ChangeWeight { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
