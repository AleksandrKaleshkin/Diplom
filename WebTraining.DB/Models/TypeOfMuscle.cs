using System.ComponentModel.DataAnnotations;

namespace WebTraining.DB.Models
{
    public class TypeOfMuscle
    {
        [Key]
        public int ID { get; set; }

        public string? NameType { get; set; }

        public List<TypeOfMuscle> TypeOfMuscles { get; set; } = new();
    }
}
