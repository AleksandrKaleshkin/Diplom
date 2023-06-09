using System.ComponentModel.DataAnnotations;
using WebTraining.DB.Models;

namespace WebTraining.Core.DTO
{
    public class ExerciseDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Укажите название упражнения")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 200 символов")]
        public string NameExercise { get; set; }

        public int TypeOfMuscleID { get; set; }
        public TypeOfMuscle? TypeOfMuscle { get; set; }

        [Required(ErrorMessage = "Укажите описание упражнения")]
        [StringLength(10000, MinimumLength = 1, ErrorMessage = "Длина строки должна быть от 1 до 10000 символов")]
        public string Description { get; set; }
    }
}
