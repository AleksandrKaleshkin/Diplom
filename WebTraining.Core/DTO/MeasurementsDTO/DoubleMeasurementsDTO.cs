using System.ComponentModel.DataAnnotations;
using WebTraining.DB.Models;

namespace WebTraining.Core.DTO.MeasurementsDTO
{
    public class DoubleMeasurementsDTO
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int MuscleId { get; set; }

        [Range(1, 250, ErrorMessage = "Недопустимое значение")]
        public float LeftValue { get; set; }

        [Range(1, 250, ErrorMessage = "Недопустимое значение")]
        public float RightValue { get; set; }

        public float? LeftChange { get; set; }

        public float? RightChange { get; set; }

        public string UserId { get; set; }
    }
}
