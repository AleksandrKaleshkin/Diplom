using System.ComponentModel.DataAnnotations;
using WebTraining.DB.Models;

namespace WebTraining.Core.DTO
{
    public class NotepadDTO
    {
        public int ID { get; set; }

        public DateTime DateNote { get; set; }

        public string? Description { get; set; }

        public string UserId { get; set; }

        public User? User { get; set; }
    }
}
