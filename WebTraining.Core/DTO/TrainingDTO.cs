using System.ComponentModel.DataAnnotations;

namespace WebTraining.Core.DTO
{
    public class TrainingDTO
    {
        [Key]
        public int ID { get; set; }

        public string? NameTraining { get; set; }

        public DateTime DateTraining { get; set; }
    }
}
