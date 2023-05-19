using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        IUnitOfWork Database { get; set; }

        public ExerciseService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public void AddExercise(ExerciseDTO exerciseDTO, int type)
        {
            TypeOfMuscle typeMusc = Database.Type.Get(type);
            exerciseDTO.TypeOfMuscleID = type;
            exerciseDTO.TypeOfMuscle = typeMusc;

            Exercise exercise = new Exercise
            {
                NameExercise = exerciseDTO.NameExercise,
                Description = exerciseDTO.Description,
                TypeOfMuscleID= typeMusc.ID,
                TypeOfMuscle=typeMusc,
                NameImage1 = exerciseDTO.NameImage1,
                NameImage2 = exerciseDTO.NameImage2,
                NameImage3 = exerciseDTO.NameImage3,
                PathImage1 = exerciseDTO.PathImage1,
                PathImage2 = exerciseDTO.PathImage2,
                PathImage3 = exerciseDTO.PathImage3
            };
            Database.Exercises.Create(exercise);
            Database.Save();
        }

        public void DeleteExercise(int id)
        {
            if (id==0)
            {
                throw new ValidationException("Упражнение не найден");
            }
            ExerciseDTO exercise= GetExercise(id);          
            Database.Exercises.Delete(id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public ExerciseDTO GetExercise(int id)
        {
            if (id==0)
            {
                throw new ValidationException();
            }
            var exercise = Database.Exercises.Get(id);
            if (exercise==null)
            {
                throw new ValidationException();
            }
            return new ExerciseDTO
            {
                ID= exercise.ID,
                Description = exercise.Description,
                TypeOfMuscleID= exercise.TypeOfMuscleID,
                NameImage1 = exercise.NameImage1,
                NameImage2 = exercise.NameImage2,
                NameImage3 = exercise.NameImage3,
                PathImage1 = exercise.PathImage1,
                PathImage2 = exercise.PathImage2,
                PathImage3 = exercise.PathImage3,
                NameExercise = exercise.NameExercise
            };
        }

        public IEnumerable<ExerciseDTO> GetExercises()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Exercise, ExerciseDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Exercise>, List<ExerciseDTO>>(Database.Exercises.GetAll());
        }

        public void UpdateExercise(ExerciseDTO exerciseDTO, int type)
        {
            TypeOfMuscle typeMusc = Database.Type.Get(type);
            exerciseDTO.TypeOfMuscleID = type;
            exerciseDTO.TypeOfMuscle = typeMusc;
            var exercise = Database.Exercises.Get(exerciseDTO.ID);
            exercise.NameExercise = exerciseDTO.NameExercise;
            exercise.Description= exerciseDTO.Description;
            exercise.TypeOfMuscleID = typeMusc.ID;
            exercise.TypeOfMuscle = typeMusc;
            exercise.NameImage1 = exerciseDTO.NameImage1;
            exercise.NameImage2 = exerciseDTO.NameImage2;
            exercise.NameImage3 = exerciseDTO.NameImage3;
            exercise.PathImage1= exerciseDTO.PathImage1;
            exercise.PathImage2 = exerciseDTO.PathImage2;
            exercise.PathImage3 = exerciseDTO.PathImage3;
            Database.Exercises.Update(exercise);
            Database.Save();
        }

        public IEnumerable<TypeOfMuscleDTO> GetTypeOfMuscles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeOfMuscle, TypeOfMuscleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TypeOfMuscle>, List<TypeOfMuscleDTO>>(Database.Type.GetTypes());            
        }
    }
}
