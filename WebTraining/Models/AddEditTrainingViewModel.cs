using WebTraining.Core.DTO;
using WebTraining.Core.Models;

namespace WebTraining.Models
{
    public class AddEditTrainingViewModel
    {
        public TrainingDTO TrainingDTO { get; set; }
        public UserList Users { get; set; }
    }
}
