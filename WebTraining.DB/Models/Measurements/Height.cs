namespace WebTraining.DB.Models.Measurements
{
    public class Height
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public float ValueHeight { get; set; }

        public float ChangeHeight { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
