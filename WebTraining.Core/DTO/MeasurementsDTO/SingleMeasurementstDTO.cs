using System.ComponentModel.DataAnnotations;

namespace WebTraining.Core.DTO.MeasurementsDTO
{
    public class SingleMeasurementstDTO
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int MuscleId { get; set; }

        [Required(ErrorMessage = "Не указано значение")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Не верный формат числа. Пример: ##,##")]
        public string Value { get; set; }

        public float Change { get; set; } = 0;

        public string UserId { get; set; }
    }
}
