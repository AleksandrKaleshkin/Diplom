using System.ComponentModel.DataAnnotations;


namespace WebTraining.DB.Models
{
    public class ImageExercise
    {
        [Key]
        public int ID { get; set; }

        public string NameImage { get; set; }

        public string PathImage { get; set; }

        public int ExerciseID { get; set; }
        public Exercise Exercise { get; set; }
    }
}
