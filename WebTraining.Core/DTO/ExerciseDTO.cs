using System.ComponentModel.DataAnnotations;
using WebTraining.DB.Models;

namespace WebTraining.Core.DTO
{
    public class ExerciseDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Укажите название упражнения")]
        public string NameExercise { get; set; }

        public int TypeOfMuscleID { get; set; }
        public TypeOfMuscle? TypeOfMuscle { get; set; }

        [Required(ErrorMessage = "Укажите описание упражнения")]
        public string Description { get; set; }

        public string? NameImage1 { get; set; }
        public string? PathImage1 { get; set; }

        public string? NameImage2 { get; set; }
        public string? PathImage2 { get; set; }

        public string? NameImage3 { get; set; }
        public string? PathImage3 { get; set; }
    }
}
