using System.ComponentModel.DataAnnotations;

namespace WebTraining.Core.DTO
{
    public class TrainingDTO
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Укажите название тренировки")]
        public string NameTraining { get; set; }

        [Required(ErrorMessage = "Укажите дату тренировки")]
        public DateTime DateTraining { get; set; }
    }
}
