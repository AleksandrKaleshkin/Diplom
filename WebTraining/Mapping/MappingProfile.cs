using AutoMapper;
using System.Collections.Generic;
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

            CreateMap<Notepad, NotepadDTO>();

            CreateMap<TrainingExercise, TrainingExerciseDTO>();
            CreateMap<Training, TrainingDTO>();

            CreateMap<DoubleMeasurements, DoubleMeasurementsDTO>();
            CreateMap<SingleMeasurements,SingleMeasurementstDTO>();
            CreateMap<MusclesMeasurements, MusclesMeasurementsDTO>();


        }
    }
}
