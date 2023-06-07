using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

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

        public List<MusclesMeasurements> InitilizerMusclesMeasurements()
        {
            List<MusclesMeasurements> muscles = new List<MusclesMeasurements>
        {
                new MusclesMeasurements { ID = 1, Name = "Вес" },
                new MusclesMeasurements { ID = 2, Name = "Рост" },
                new MusclesMeasurements { ID = 3, Name = "Грудь" },
                new MusclesMeasurements { ID = 4, Name = "Талия" },
                new MusclesMeasurements { ID = 5, Name = "Ягодицы" },
                new MusclesMeasurements { ID = 6, Name = "Битцепс" },
                new MusclesMeasurements { ID = 7, Name = "Предплечье" },
                new MusclesMeasurements { ID = 8, Name = "Ноги"},
                new MusclesMeasurements { ID = 9, Name = "Голень"}};
            return muscles;
        }
    }
}

