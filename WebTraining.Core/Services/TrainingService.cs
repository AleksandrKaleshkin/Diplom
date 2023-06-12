using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO;
using WebTraining.Core.Interfaces;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;

namespace WebTraining.Core.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository<Training> service;
        private readonly IMapper mapper;

        public TrainingService(ITrainingRepository<Training> service, IMapper mapper)
        {
            this.mapper = mapper;
            this.service = service;
        }

        public void AddTraing(TrainingDTO trainingDTO)
        {
            Training training = new Training
            {
                NameTraining = trainingDTO.NameTraining,
                DateTraining = trainingDTO.DateTraining.ToUniversalTime(),
                UserId=trainingDTO.UserId,
                User= service.GetUser(trainingDTO.UserId)              
            };
            service.Create(training);
        }

        public void DeleteTraining(int id)
        {
            if (id!= 0)
            {               
                service.Delete(id);
            }
            else
            {
                throw new ValidationException("Тренировка не найдена");
            }
        }

        public TrainingDTO GetTraining(int id)
        {
            if (id == 0)
            {
                throw new ValidationException();
            }
            var training = service.Get(id);
            if (training==null)
            {
                throw new ValidationException();
            }
            return new TrainingDTO { 
                ID= training.ID,
                NameTraining = training.NameTraining,
                DateTraining = training.DateTraining };
        }

        public IEnumerable<TrainingDTO> GetTrainingss()
        {
            var training_list = service.GetAll();
            return mapper.Map<IEnumerable<TrainingDTO>>(training_list);
        }

        public IEnumerable<TrainingDTO> GetTrainings()
        {
            IEnumerable<TrainingDTO> training = GetTrainingss();
            foreach (var item in training)
            {
                item.DateTraining = item.DateTraining.ToLocalTime();
            }
            return training;
        }


        public IEnumerable<TrainingDTO> GetNeedTraining(User user)
        {
            IEnumerable<TrainingDTO> training = GetTrainings();
            IEnumerable<TrainingDTO> needtraining = training.Where(x => x.UserId == user.Id);
            return needtraining;
        }

        public void UpdateTraining(TrainingDTO trainingDTO)
        {
                var training = service.Get(trainingDTO.ID);
                training.NameTraining = trainingDTO.NameTraining;
                training.DateTraining = trainingDTO.DateTraining.ToUniversalTime();
                training.UserId = trainingDTO.UserId;
                service.Update(training);

            
        }

        public IEnumerable<User> GetAllUsers()
        {
            return service.GetAllUsers();
        }
    }
}
