using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.DataContext;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class TrainingService : ITrainingService
    {
        IUnitOfWork Database { get; set; }

        public TrainingService(IUnitOfWork unit)
        {
            Database = unit;
        }

        public void AddTraing(TrainingDTO trainingDTO)
        {
            Training training = new Training
            {
                NameTraining = trainingDTO.NameTraining,
                DateTraining = trainingDTO.DateTraining.ToUniversalTime()
            };
            Database.Training.Create(training);
            Database.Save();


        }

        public void DeleteTraining(int id)
        {
            if (id!= 0)
            {
                Database.Training.Delete(id);
                Database.Save();
            }
            else
            {
                throw new ValidationException("Тренировка не найдена");
            }
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public TrainingDTO GetTraining(int id)
        {
            if (id == 0)
            {
                throw new ValidationException();
            }
            var training = Database.Training.Get(id);
            if (training==null)
            {
                throw new ValidationException();
            }
            return new TrainingDTO { 
                NameTraining = training.NameTraining,
                DateTraining = training.DateTraining };
        }

        public IEnumerable<TrainingDTO> GetTrainings()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Training, TrainingDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Training>, List<TrainingDTO>>(Database.Training.GetAll());
        }

        public void UpdateTraining(TrainingDTO trainingDTO)
        {
            var training = Database.Training.Get(trainingDTO.ID);
            training.NameTraining = trainingDTO.NameTraining;
            training.DateTraining = trainingDTO.DateTraining;
            Database.Training.Update(training);
            Database.Save();
        }


    }
}
