using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.InitializeData;

namespace WebTraining.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        IExerciseRepository<Exercise> service { get; set; }


        public ExerciseService(IExerciseRepository<Exercise> service)
        {
            this.service = service;
        }

        public void AddExercise(ExerciseDTO exerciseDTO)
        {
            var exercises = GetExercises();
            Exercise exercise = new Exercise
            {
                NameExercise = exerciseDTO.NameExercise,
                Description = exerciseDTO.Description,
                TypeOfMuscleID= exerciseDTO.TypeOfMuscleID,
                TypeOfMuscle=service.GetType(exerciseDTO.TypeOfMuscleID)
            };
            service.Create(exercise);
            service.Save();
        }


        public void DeleteExercise(int id)
        {
            if (id==0)
            {
                throw new ValidationException("Упражнение не найден");
            }
            ExerciseDTO exercise= GetExercise(id);
            service.Delete(id);
            service.Save();
        }



        public ExerciseDTO GetExercise(int id)
        {
            if (id==0)
            {
                throw new ValidationException();
            }
            var exercise = service.Get(id);
            if (exercise==null)
            {
                throw new ValidationException();
            }
            return new ExerciseDTO
            {
                ID= exercise.ID,
                Description = exercise.Description,
                TypeOfMuscle= service.GetType(exercise.TypeOfMuscleID),
                TypeOfMuscleID= exercise.TypeOfMuscleID,
                NameExercise = exercise.NameExercise
            };
        }

        public IEnumerable<ExerciseDTO> GetExercises()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Exercise, ExerciseDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Exercise>, List<ExerciseDTO>>(service.GetAll());
        }

        public void UpdateExercise(ExerciseDTO exerciseDTO)
        {
            exerciseDTO.TypeOfMuscle = service.GetType(exerciseDTO.TypeOfMuscleID);
            var exercise = service.Get(exerciseDTO.ID);
            exercise.NameExercise = exerciseDTO.NameExercise;
            exercise.Description= exerciseDTO.Description;
            exercise.TypeOfMuscleID = exerciseDTO.TypeOfMuscleID;
            exercise.TypeOfMuscle = exercise.TypeOfMuscle;
            service.Update(exercise);
            service.Save();
        }

        public IEnumerable<TypeOfMuscleDTO> GetTypeOfMuscles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeOfMuscle, TypeOfMuscleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TypeOfMuscle>, List<TypeOfMuscleDTO>>(service.GetTypes());            
        }

        public void AddPicture(ImageExerciseDTO image)
        {
            ImageExercise imageExercise = new ImageExercise
            {
                NameImage = image.NameImage,
                PathImage = image.PathImage,
                ExerciseID = image.ExerciseID,
                Exercise = service.Get(image.ExerciseID)
            };
            service.AddPicture(imageExercise);
        }

        public IEnumerable<ImageExerciseDTO> GetImageExercises(ExerciseDTO exercise)
        {
            var exercisedb = service.Get(exercise.ID);
            return GetImages().Where(x=>x.ExerciseID==exercisedb.ID);
        }

        public void DeleteImage(int id)
        {
            service.DeleteImage(id);
        }

        public IEnumerable<ImageExerciseDTO> GetImages()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ImageExercise, ImageExerciseDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ImageExercise>, List<ImageExerciseDTO>>(service.GetImages());
        }
    }
}
