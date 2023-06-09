using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.Core.Interfaces.IMeasurements;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.Core.Services.MeasurementsService
{
    public class SingleMeasurementsService : ISingleMeasurementstService
    {
        private readonly ISingleMeasRepository<SingleMeasurements> service;    

        public SingleMeasurementsService(ISingleMeasRepository<SingleMeasurements> service)
        {
            this.service= service;
        }

        public void AddMeasurement(SingleMeasurementstDTO measDTO, User user)
        {
            var meass = GetNeedMeasurements(user, measDTO.MuscleId).OrderBy(x => x.Date);
            if (meass.Count() > 0)
            {
                var premeas = GetPreMeasurement(meass.Last());
                if (premeas != null)
                {
                    SingleMeasurements meas = new SingleMeasurements
                    {
                        Date = measDTO.Date.ToUniversalTime(),
                        Value = measDTO.Value,
                        Change = (float)Math.Round(measDTO.Value - premeas.Value, 4),
                        UserId = measDTO.UserId,
                        User=user,
                        MuscleId=measDTO.MuscleId,
                        TypeOfMuscle=service.GetMuscles(measDTO.MuscleId)
                    };
                    service.Create(meas);
                    service.Save();
                }
            }
            else
            {
                SingleMeasurements meas = new SingleMeasurements
                {
                    Date = measDTO.Date.ToUniversalTime(),
                    Value = measDTO.Value,
                    Change = 0,
                    UserId = measDTO.UserId,
                    MuscleId = measDTO.MuscleId,
                    TypeOfMuscle = service.GetMuscles(measDTO.MuscleId)
                };
                service.Create(meas);
                service.Save();
            }
        }

        public void DeleteMeasurement(int id)
        {
            if (id != 0)
            {
                service.Delete(id);
                service.Save();
            }
            else
            {
                throw new ValidationException("Измерение не найдено");
            }
        }



        public SingleMeasurementstDTO GetMeasurement(int id)
        {
            if (id != 0)
            {
                var meas = service.Get(id);
                if (meas != null)
                {

                    return new SingleMeasurementstDTO
                    {
                        Date = meas.Date,
                        Value = meas.Value,
                        Change = meas.Change,
                        MuscleId = meas.MuscleId,
                        UserId = meas.UserId
                    };
                }
                throw new ValidationException("Измерение не найдено");
            }
            throw new ValidationException("Измерение не найдено");
        }

        public IEnumerable<SingleMeasurementstDTO> GetMeasurements()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SingleMeasurements, SingleMeasurementstDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<SingleMeasurements>, List<SingleMeasurementstDTO>>(service.GetAll());
        }

        public IEnumerable<SingleMeasurementstDTO> GetNeedMeasurements(User user, int type)
        {
            IEnumerable<SingleMeasurementstDTO> needmeas = GetMeasurements().Where(y=>y.MuscleId == type).Where(x => x.UserId == user.Id);
            foreach (var item in needmeas)
            {
                item.Date = item.Date.ToLocalTime();
            }
            return needmeas;
        }

        public void UpdateMeasurement(SingleMeasurementstDTO measDTO, User user)
        {
            var meas = service.Get(measDTO.ID);
            var meass = GetNeedMeasurements(user, measDTO.MuscleId);
            if (meas != null)
            {
                if (meass.Count() != 1)
                {
                    SingleMeasurementstDTO premeas = GetPreMeasurement(meass.Reverse().Skip(1).FirstOrDefault());
                    meas.Date = measDTO.Date;
                    meas.Value = measDTO.Value;
                    meas.Change = (float)Math.Round((measDTO.Value - premeas.Value), 4);
                    meas.MuscleId = measDTO.MuscleId;
                    service.Update(meas);
                    service.Save();
                }
                else
                {
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.Value = measDTO.Value;
                    meas.Change = 0;
                    meas.MuscleId = measDTO.MuscleId;
                    service.Update(meas);
                    service.Save();
                }
            }
        }

        private SingleMeasurementstDTO GetPreMeasurement(SingleMeasurementstDTO premeas)
        {
            return new SingleMeasurementstDTO
            {
                ID = premeas.ID,
                Date = premeas.Date,
                Value = premeas.Value,
                Change = premeas.Change,
                UserId = premeas.UserId,
            };
        }

        private IEnumerable<MusclesMeasurementsDTO> GetTypeOfMuscles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MusclesMeasurements, MusclesMeasurementsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<MusclesMeasurements>, List<MusclesMeasurementsDTO>>(service.GetTypes());
        }

        public MusclesMeasurementsDTO GetTypeOfMuscle(string type)
        {
            return GetTypeOfMuscles().FirstOrDefault(x => x.Name == type);
        }
    }
}
