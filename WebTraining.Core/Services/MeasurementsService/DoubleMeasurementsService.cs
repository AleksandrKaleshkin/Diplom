using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.Core.Interfaces.IMeasurements;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.Core.Services.MeasurementsService
{
    public class DoubleMeasurementsService : IDoubleMeasurementsService
    {
        private readonly IDoubleMeasRepository<DoubleMeasurements> service;

        public DoubleMeasurementsService(IDoubleMeasRepository<DoubleMeasurements> service)
        {
            this.service = service;
        }

        public void AddMeasurement(DoubleMeasurementsDTO measDTO, User user)
        {
            var meass = GetNeedMeasurements(user, measDTO.MuscleId).OrderBy(x=>x.Date);
            if (meass.Count()>0)
            {
                var premeas = GetPreMeasurement(meass.Last());
                if (premeas != null)
                {
                    DoubleMeasurements meas = new DoubleMeasurements
                    {
                        Date = measDTO.Date.ToUniversalTime(),
                        LeftValue = measDTO.LeftValue,
                        RightValue = measDTO.RightValue,
                        LeftChange = (float)Math.Round((measDTO.LeftValue - premeas.LeftValue), 3),
                        RightChange = (float)Math.Round((measDTO.RightValue - premeas.RightValue),3),
                        UserId = measDTO.UserId,
                        MuscleId=measDTO.MuscleId,
                        TypeOfMuscle=service.GetMuscles(measDTO.MuscleId)
                    };
                    service.Create(meas);
                    service.Save();
                }
            }
            else
            {                
                    DoubleMeasurements meas = new DoubleMeasurements
                    {
                        Date = measDTO.Date.ToUniversalTime(),
                        LeftValue = measDTO.LeftValue,
                        RightValue = measDTO.RightValue,
                        LeftChange = 0,
                        RightChange = 0,
                        UserId = measDTO.UserId,
                        MuscleId=measDTO.MuscleId
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

        public DoubleMeasurementsDTO GetMeasurement(int id)
        {
            if (id!= 0)
            {
                var meas = service.Get(id);
                    if (meas!=null)
                    {
                    return new DoubleMeasurementsDTO
                    {
                        Date = meas.Date,
                        LeftValue = meas.LeftValue,
                        RightValue = meas.RightValue,
                        LeftChange = meas.LeftChange,
                        RightChange = meas.RightChange,
                        UserId = meas.UserId,
                        MuscleId =meas.MuscleId
                    };
                    }
            }
            throw new ValidationException("Измерение не найдено");
        }

        public IEnumerable<DoubleMeasurementsDTO> GetMeasurements()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DoubleMeasurements, DoubleMeasurementsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<DoubleMeasurements>, List<DoubleMeasurementsDTO>>(service.GetAll());
        }

        public IEnumerable<DoubleMeasurementsDTO> GetNeedMeasurements(User user, int type)
        {
            IEnumerable<DoubleMeasurementsDTO> needmeas = GetMeasurements().Where(x => x.UserId == user.Id).Where(y => y.MuscleId == type);
            foreach (var item in needmeas)
            {
                item.Date = item.Date.ToLocalTime();
            }
            return needmeas;
        }


        public void UpdateMeasurement(DoubleMeasurementsDTO measDTO, User user)
        {
            var meas = service.Get(measDTO.ID);
            var meass = GetNeedMeasurements(user, measDTO.MuscleId);
            if (meas != null)
            {
                if (meass.Count()!=1)
                {
                    DoubleMeasurementsDTO premeas = GetPreMeasurement(meass.Reverse().Skip(1).FirstOrDefault());

                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.LeftValue = measDTO.LeftValue;
                    meas.RightValue = measDTO.RightValue;
                    meas.MuscleId= measDTO.MuscleId;
                    meas.LeftChange = (float)Math.Round((measDTO.LeftValue - premeas.LeftValue),3);
                    meas.RightChange = (float)Math.Round((measDTO.RightValue - premeas.RightValue),3);
                    service.Update(meas);
                    service.Save();
                }
                else
                {
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.MuscleId = measDTO.MuscleId;
                    meas.LeftValue = measDTO.LeftValue;
                    meas.RightValue = measDTO.RightValue;
                    meas.LeftChange = 0;
                    meas.RightChange = 0;
                    service.Update(meas);
                    service.Save();
                }
            }
        }

        private DoubleMeasurementsDTO GetPreMeasurement(DoubleMeasurementsDTO premeas)
        {
            return new DoubleMeasurementsDTO
            {
                ID = premeas.ID,
                Date = premeas.Date,
                LeftValue = premeas.LeftValue,
                RightValue = premeas.RightValue,
                LeftChange = premeas.LeftChange,
                RightChange = premeas.RightChange,
                UserId = premeas.UserId,
                MuscleId =premeas.MuscleId
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
