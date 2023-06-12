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
        private readonly IExerciseRepository<Exercise> service;
        private readonly IMapper mapper;


        public ExerciseService(IExerciseRepository<Exercise> service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }

        public Exercise AddExercise(ExerciseDTO exerciseDTO)
        {
            var exercises = GetExercises();
            Exercise exercise = new Exercise
            {
                ID=++exercises.OrderBy(x=>x.ID).Last().ID,
                NameExercise = exerciseDTO.NameExercise,
                Description = exerciseDTO.Description,
                TypeOfMuscleID= exerciseDTO.TypeOfMuscleID,
                TypeOfMuscle=service.GetType(exerciseDTO.TypeOfMuscleID)
            };
            service.Create(exercise);
            service.Save();
            return exercise;
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
            var exercise_list = service.GetAll();
            return mapper.Map<IEnumerable<ExerciseDTO>>(exercise_list);
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
            var muscle_list = service.GetTypes();
            return mapper.Map<IEnumerable<TypeOfMuscleDTO>>(muscle_list);            
        }

        public void AddPicture(ImageExerciseDTO image)
        {
            ImageExercise imageExercise = new ImageExercise
            {
                ID = ++GetImages().OrderBy(m=>m.ID).Last().ID,
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
            var image_list = service.GetImages();
            return mapper.Map<IEnumerable<ImageExerciseDTO>>(image_list);
        }

        public void UpdatePicture(ImageExerciseDTO image)
        {
            var imagedb = service.GetNeedImage(image.ID);
            imagedb.NameImage = image.NameImage;
            imagedb.PathImage = image.PathImage;
            service.UpdatePic(imagedb);

        }
    }
}
