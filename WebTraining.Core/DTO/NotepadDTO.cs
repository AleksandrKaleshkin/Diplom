using System.ComponentModel.DataAnnotations;

namespace WebTraining.Core.DTO
{
    public class NotepadDTO
    {
        public int ID { get; set; }

        public DateTime DateNote { get; set; }

        public string? Description { get; set; }
    }
}
