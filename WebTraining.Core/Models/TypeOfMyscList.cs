using Microsoft.AspNetCore.Mvc.Rendering;
using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Models
{
    public class TypeOfMyscList
    {
        public TypeOfMyscList(List<TypeOfMuscleDTO> typeOfMuscles)
        {            
            MyscList = new SelectList(typeOfMuscles,"ID", "NameType");
        }
        public SelectList? MyscList { get; private set; }
    }
}
