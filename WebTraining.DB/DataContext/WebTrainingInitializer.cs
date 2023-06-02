using WebTraining.DB.Models;

namespace WebTraining.DB.DataContext
{
    public class WebTrainingInitializer
    {
        public List<TypeOfMuscle> InitilizerType()
        {
            List<TypeOfMuscle> muscles = new List<TypeOfMuscle>
        {
                new TypeOfMuscle { ID = 1, NameType = "Битцепс" },
                new TypeOfMuscle { ID = 2, NameType = "Пресс" },
                new TypeOfMuscle { ID = 3, NameType = "Трицепс" },
                new TypeOfMuscle { ID = 4, NameType = "Плечи" },
                new TypeOfMuscle { ID = 5, NameType = "Предплечья" },
                new TypeOfMuscle { ID = 6, NameType = "Голень" },
                new TypeOfMuscle { ID = 7, NameType = "Ноги" },
                new TypeOfMuscle { ID = 8, NameType = "Спина" },
                new TypeOfMuscle { ID = 9, NameType = "Грудь" }};
            return muscles;
        }   
    }
}

