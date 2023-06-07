using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private WebTrainingContext db;


        public TrainingService(IUnitOfWork unit, WebTrainingContext db)
        {
            Database = unit;
            this.db = db;
        }

        public void AddTraing(TrainingDTO trainingDTO)
        {
            Training training = new Training
            {
                NameTraining = trainingDTO.NameTraining,
                DateTraining = trainingDTO.DateTraining.ToUniversalTime(),
                UserId=trainingDTO.UserId,
                User= GetUser(trainingDTO.UserId)
                
                
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
                ID= training.ID,
                NameTraining = training.NameTraining,
                DateTraining = training.DateTraining };
        }

        public IEnumerable<TrainingDTO> GetTrainings()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Training, TrainingDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Training>, List<TrainingDTO>>(Database.Training.GetAll());
        }

        public IEnumerable<TrainingDTO> GetNeedTraining(User user)
        {
            IEnumerable<TrainingDTO> training = GetTrainings();
            IEnumerable<TrainingDTO> needtraining = training.Where(x => x.UserId == user.Id);
            return needtraining;
        }

        public void UpdateTraining(TrainingDTO trainingDTO)
        {
            var training = Database.Training.Get(trainingDTO.ID);
            training.NameTraining = trainingDTO.NameTraining;
            training.DateTraining = trainingDTO.DateTraining.ToUniversalTime();
            training.UserId = trainingDTO.UserId;
            Database.Training.Update(training);
            Database.Save();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }

        private User GetUser(string id)
        {
            return db.Users.Find(id);
        }
    }
}
