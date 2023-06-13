using AutoMapper;
using WebTraining.Core.DTO;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {   
            CreateMap<TypeOfMuscle, TypeOfMuscleDTO>().ReverseMap();
            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
            CreateMap<ImageExercise, ImageExerciseDTO>().ReverseMap();

            CreateMap<Notepad, NotepadDTO>().ReverseMap();

            CreateMap<TrainingExercise, TrainingExerciseDTO>().ReverseMap();
            CreateMap<Training, TrainingDTO>().ReverseMap();

            CreateMap<DoubleMeasurements, DoubleMeasurementsDTO>().ReverseMap();
            CreateMap<SingleMeasurements,SingleMeasurementstDTO>().ReverseMap();
            CreateMap<MusclesMeasurements, MusclesMeasurementsDTO>().ReverseMap();


        }
    }
}
