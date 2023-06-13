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
            trainingDTO.DateTraining = trainingDTO.DateTraining.ToUniversalTime();
            trainingDTO.User = service.GetUser(trainingDTO.UserId);
            Training training = mapper.Map<Training>(trainingDTO);
            service.Create(training);
        }

        public void DeleteTraining(int id)
        {
            if (id== 0)
            {
                throw new ValidationException("Тренировка не найдена");
            }
            service.Delete(id);



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
            return mapper.Map<TrainingDTO>(training);
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
            return training.Where(x => x.DateTraining > DateTime.Now);
        }


        public IEnumerable<TrainingDTO> GetUserTraining(User user)
        {
            IEnumerable<TrainingDTO> training = GetTrainingss();
            IEnumerable<TrainingDTO> needtraining = training.Where(x => x.UserId == user.Id).Where(x=>x.DateTraining>DateTime.UtcNow);
            foreach (var item in needtraining)
            {
                item.DateTraining = item.DateTraining.ToLocalTime();
            }
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

        public IEnumerable<TrainingDTO> GetUserPastTraining(User user)
        {
            IEnumerable<TrainingDTO> training = GetTrainingss();
            IEnumerable<TrainingDTO> needtraining = training.Where(x => x.UserId == user.Id);
            foreach (var item in needtraining)
            {
                item.DateTraining = item.DateTraining.ToLocalTime();
            }
            return needtraining.Where(x => x.DateTraining < DateTime.Now);
        }

        public IEnumerable<TrainingDTO> GetPastTrainings()
        {
            IEnumerable<TrainingDTO> training = GetTrainingss();
            foreach (var item in training)
            {
                item.DateTraining = item.DateTraining.ToLocalTime();
            }
            return training.Where(x=>x.DateTraining<DateTime.Now);
        }
    }
}
