using Microsoft.AspNetCore.Mvc.Rendering;
using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Models
{
    public class ExerciseList
    {
        public ExerciseList(List<ExerciseDTO> exercises)
        {
            Exercises= new SelectList(exercises, "ID", "NameExercise");
        }
        public SelectList Exercises { get; set; }
    }
}
