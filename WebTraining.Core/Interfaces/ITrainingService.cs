﻿using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Interfaces
{
    public interface ITrainingService
    {
        TrainingDTO GetTraining(int id);
        void DeleteTraining(int id);
        void AddTraing(TrainingDTO training);
        IEnumerable<TrainingDTO> GetTrainings();
        IEnumerable<TrainingDTO> GetNeedTraining(User user);
        IEnumerable<User> GetAllUsers();
        void UpdateTraining(TrainingDTO training);

    }
}
