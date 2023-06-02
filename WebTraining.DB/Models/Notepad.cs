using System.ComponentModel.DataAnnotations;

namespace WebTraining.DB.Models
{
    public class Notepad
    {
        [Key]
        public int ID { get; set; }

        public DateTime DateNote { get; set; }

        public string? Description { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
