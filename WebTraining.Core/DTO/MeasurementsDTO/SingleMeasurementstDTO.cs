using System.ComponentModel.DataAnnotations;

namespace WebTraining.Core.DTO.MeasurementsDTO
{
    public class SingleMeasurementstDTO
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int MuscleId { get; set; }

        [Required(ErrorMessage = "Не указано значение")]
        [Range(9, 250, ErrorMessage = "Недопустимое значение")]
        public float Value { get; set; }

        public float? Change { get; set; }

        public string UserId { get; set; }
    }
}
