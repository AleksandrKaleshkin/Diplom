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
        ITERepository<TrainingExercise> service { get; set; }

        public TrainingExerciseService(ITERepository<TrainingExercise> service)
        {
            this.service = service;
        }

        public void AddExercise(TrainingExerciseDTO model)
        {
            TrainingExercise trainingExercise = new TrainingExercise
            {                
                TrainingId = model.TrainingId,
                ExerciseId = model.ExerciseId,
                Sets = model.Sets,
                Repetitions = model.Repetitions
            };
            service.Create(trainingExercise);
        }

        public void DeleteExercise(int id)
        {
            if (id != 0)
            {
                service.Delete(id);
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
            return mapper.Map<IEnumerable<TrainingExercise>, List<TrainingExerciseDTO>>(service.GetAll());
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
            var exercise = service.Get(exerciseDTO.ID);
            exercise.TrainingId = exerciseDTO.TrainingId;
            exercise.ExerciseId = exerciseDTO.ExerciseId;
            exercise.Sets = exerciseDTO.Sets;
            exercise.Repetitions = exerciseDTO.Repetitions;
            service.Update(exercise);

        }

        public TrainingExerciseDTO GetNeedExercise(int id)
        {
            if (id != 0)
            {
                var exercise = service.Get(id);
                if (exercise != null)
                {
                    return new TrainingExerciseDTO
                    {
                        ID= exercise.ID,                        
                        ExerciseId = exercise.ExerciseId,
                        TrainingId = exercise.TrainingId,
                        Repetitions = exercise.Repetitions,
                        Sets = exercise.Sets
                    };
                }
            }
            throw new ValidationException();
        }

        public IEnumerable<ExerciseDTO> GetExerciseList()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Exercise, ExerciseDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Exercise>, List<ExerciseDTO>>(service.GetAllExercise());
        }
    }
}
