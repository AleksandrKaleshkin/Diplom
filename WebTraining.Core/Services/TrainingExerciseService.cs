using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class TrainingExerciseService : ITrainingExerciseService
    {
        IUnitOfWork Database { get; set; }

        public TrainingExerciseService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public void AddExercise(TrainingExerciseDTO exercise)
        {
            TrainingExercise trainingExercise = new TrainingExercise
            {
                TrainingId = exercise.TrainingId,
                ExerciseId = exercise.ExerciseId,
                Sets = exercise.Sets,
                Repetitions = exercise.Repetitions
            };
            Database.TrainingExercise.Create(trainingExercise);
            Database.Save();
        }

        public void DeleteExercise(int id)
        {
            if (id != 0)
            {
                Database.TrainingExercise.Delete(id);
                Database.Save();
            }
            else
            {
                throw new ValidationException("Тренировка не найдена");
            }
        }

        public TrainingExerciseDTO GetExercise(int id)
        {
            return new TrainingExerciseDTO { TrainingId = id };
        }

        public IEnumerable<TrainingExerciseDTO> GetExercises()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TrainingExercise, TrainingExerciseDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TrainingExercise>, List<TrainingExerciseDTO>>(Database.TrainingExercise.GetAll());
        }

        public List<TrainingExerciseDTO> GetNeedExercises(int id)
        {
            IEnumerable<TrainingExerciseDTO> training = GetExercises();
            List<TrainingExerciseDTO> exercises = new List<TrainingExerciseDTO>();
            foreach (var item in training)
            {
                if (item.TrainingId == id)
                {
                    exercises.Add(item);
                }
            }
            return exercises;
        }
        public void UpdateExercise(TrainingExerciseDTO exerciseDTO)
        {
            var exercise = Database.TrainingExercise.Get(exerciseDTO.ID);
            exercise.TrainingId = exerciseDTO.TrainingId;
            exercise.ExerciseId = exerciseDTO.ExerciseId;
            exercise.Sets = exerciseDTO.Sets;
            exercise.Repetitions = exerciseDTO.Repetitions;
            Database.TrainingExercise.Update(exercise);
            Database.Save();

        }

        public TrainingExerciseDTO GetNeedExercise(int id)
        {
            if (id != 0)
            {
                var exercise = Database.TrainingExercise.Get(id);
                if (exercise != null)
                {
                    return new TrainingExerciseDTO
                    {
                        ExerciseId = exercise.ExerciseId,
                        TrainingId = exercise.TrainingId,
                        Repetitions = exercise.Repetitions,
                        Sets = exercise.Sets
                    };
                }
            }
            throw new ValidationException();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
