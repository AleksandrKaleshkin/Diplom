using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.Core.Models;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class TrainingExerciseService : ITrainingExerciseService
    {
        private readonly ITERepository<TrainingExercise> service;
        private readonly IMapper mapper;


        public TrainingExerciseService(ITERepository<TrainingExercise> service, IMapper mapper)
        {
            this.mapper = mapper;
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
            var exexercise_list = service.GetAll();
            return mapper.Map<IEnumerable<TrainingExerciseDTO>>(exexercise_list);
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
            var exerciselist = service.GetAllExercise();
            return mapper.Map<IEnumerable<ExerciseDTO>>(exerciselist);
        }
    }
}
