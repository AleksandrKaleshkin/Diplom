using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Models;

namespace WebTraining.Models
{
    public class AddEditTrainingViewModel
    {
        [Required]
        public TrainingDTO TrainingDTO { get; set; }
        
        public UserList? Users { get; set; }
    }
}
