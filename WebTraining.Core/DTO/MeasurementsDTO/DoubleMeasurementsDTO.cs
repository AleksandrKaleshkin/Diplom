using System.ComponentModel.DataAnnotations;
using WebTraining.DB.Models;

namespace WebTraining.Core.DTO.MeasurementsDTO
{
    public class DoubleMeasurementsDTO
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int MuscleId { get; set; }

        [Required (ErrorMessage ="Не указано значение") ]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Не верный формат числа. Пример: #,##")]
        public string LeftValue { get; set; }

        [Required(ErrorMessage = "Не указано значение")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Не верный формат числа. Пример: #,##")]
        public string RightValue { get; set; }

        public float? LeftChange { get; set; }

        public float? RightChange { get; set; }

        public string UserId { get; set; }
    }
}
