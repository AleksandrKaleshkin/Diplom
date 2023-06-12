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
        private readonly IExerciseRepository<Exercise> exerciseRepository;
        private readonly IMapper mapper;
        private readonly ITypeOfMuscleRepository typeOfMuscleRepository;
        private readonly IimageExerciseRepository imageExerciseRepository;


        public ExerciseService(IExerciseRepository<Exercise> repository, IMapper mapper, ITypeOfMuscleRepository typeOfMuscleRepository, IimageExerciseRepository imageExerciseRepository)
        {
            this.mapper = mapper;
            exerciseRepository = repository;
            this.typeOfMuscleRepository= typeOfMuscleRepository;
            this.imageExerciseRepository = imageExerciseRepository;
        }

        public void AddExercise(ExerciseDTO exerciseDTO)
        {
            exerciseDTO.TypeOfMuscle = typeOfMuscleRepository.GetType(exerciseDTO.TypeOfMuscleID);
            Exercise exercise = mapper.Map<Exercise>(exerciseDTO);
            exerciseRepository.Create(exercise);
        }


        public void DeleteExercise(int id)
        {
            if (id==0)
            {
                throw new ValidationException("Упражнение не найдено");
            }
            ExerciseDTO exercise= GetExercise(id);
            exerciseRepository.Delete(id);
            exerciseRepository.Save();
        }



        public ExerciseDTO GetExercise(int id)
        {
            if (id==0)
            {
                throw new ValidationException("Упражнение не найдено");
            }
            var exercise = exerciseRepository.Get(id);
            return mapper.Map<ExerciseDTO>(exercise);
        }

        public IEnumerable<ExerciseDTO> GetExercises()
        {
            var exercise_list = exerciseRepository.GetAll();
            return mapper.Map<IEnumerable<ExerciseDTO>>(exercise_list);
        }

        public void UpdateExercise(ExerciseDTO exerciseDTO)
        {
            var exercise = exerciseRepository.Get(exerciseDTO.ID);

            exercise.NameExercise = exerciseDTO.NameExercise;
            exercise.Description= exerciseDTO.Description;
            exercise.TypeOfMuscleID = exerciseDTO.TypeOfMuscleID;

            exerciseRepository.Update(exercise);
            exerciseRepository.Save();
        }

        public IEnumerable<TypeOfMuscleDTO> GetTypeOfMuscles()
        {
            var muscle_list = typeOfMuscleRepository.GetAllTypes();
            return mapper.Map<IEnumerable<TypeOfMuscleDTO>>(muscle_list);            
        }


        public void AddPicture(ImageExerciseDTO imageDTO)
        {
            imageDTO.Exercise = exerciseRepository.Get(imageDTO.ExerciseID);
            ImageExercise image = mapper.Map<ImageExercise>(imageDTO);
            imageExerciseRepository.AddPicture(image);
        }

        public IEnumerable<ImageExerciseDTO> GetImageExercises(ExerciseDTO exercise)
        {
            var exercisedb = exerciseRepository.Get(exercise.ID);
            return GetImages().Where(x=>x.ExerciseID==exercisedb.ID);
        }

        public void DeleteImage(int id)
        {
            imageExerciseRepository.DeleteImage(id);
        }

        public IEnumerable<ImageExerciseDTO> GetImages()
        {
            var image_list = imageExerciseRepository.GetImages();
            return mapper.Map<IEnumerable<ImageExerciseDTO>>(image_list);
        }

        public void UpdatePicture(ImageExerciseDTO image)
        {
            var imagedb = imageExerciseRepository.GetExerciseImage(image.ID);
            imagedb.NameImage = image.NameImage;
            imagedb.PathImage = image.PathImage;
            imageExerciseRepository.UpdatePic(imagedb);

        }
    }
}
