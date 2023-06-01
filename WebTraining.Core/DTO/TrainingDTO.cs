using System.ComponentModel.DataAnnotations;
using WebTraining.DB.Models;

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

        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
