using Microsoft.AspNetCore.Mvc.Rendering;

using WebTraining.DB.Models;

namespace WebTraining.Core.Models
{
    public class TypeOfMyscList
    {
        public TypeOfMyscList(List<TypeOfMuscle> typeOfMuscles)
        {            
            MyscList = new SelectList(typeOfMuscles,"ID", "NameType");
        }
        public SelectList? MyscList { get; private set; }
    }
}
