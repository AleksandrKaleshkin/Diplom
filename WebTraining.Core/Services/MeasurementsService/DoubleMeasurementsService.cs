using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using WebTraining.Core.DTO;
using WebTraining.Core.DTO.MeasurementsDTO;
using WebTraining.Core.Interfaces.IMeasurements;
using WebTraining.DB.Interfaces;
using WebTraining.DB.Models;
using WebTraining.DB.Models.Measurements;

namespace WebTraining.Core.Services.MeasurementsService
{
    public class DoubleMeasurementsService : IDoubleMeasurementsService
    {
        IUnitOfWork DataBase { get; set; }

        public DoubleMeasurementsService(IUnitOfWork DataBase)
        {
            this.DataBase = DataBase;
        }

        public void AddMeasurement(DoubleMeasurementsDTO measDTO, User user)
        {
            var meass = GetNeedMeasurements(user, measDTO.MuscleId).OrderBy(x=>x.Date);
            if (meass.Count()>0)
            {
                var premeas = GetPreMeasurement(meass.Last());
                var LeftValue = (float)Decimal.Parse(measDTO.LeftValue, CultureInfo.InvariantCulture);
                var RightValue = (float)Decimal.Parse(measDTO.RightValue, CultureInfo.InvariantCulture);
                var PreLeftValue = (float)Decimal.Parse(premeas.LeftValue);
                var PreRightValue = (float)Decimal.Parse(premeas.RightValue);
                if (premeas != null)
                {
                    DoubleMeasurements meas = new DoubleMeasurements
                    {
                        Date = measDTO.Date.ToUniversalTime(),
                        LeftValue = (float)LeftValue,
                        RightValue = (float)RightValue,
                        LeftChange = (float)Math.Round((LeftValue - PreLeftValue), 4),
                        RightChange = (float)Math.Round((RightValue - PreRightValue),4),
                        UserId = measDTO.UserId,
                        MuscleId=measDTO.MuscleId,
                        TypeOfMuscle=DataBase.Muscles.Get(measDTO.MuscleId)
                    };
                    DataBase.DoubleMeasurements.Create(meas);
                    DataBase.Save();
                }
            }
            else
            {                
                    DoubleMeasurements meas = new DoubleMeasurements
                    {
                        Date = measDTO.Date.ToUniversalTime(),
                        LeftValue = (float)Decimal.Parse(measDTO.LeftValue, CultureInfo.InvariantCulture.NumberFormat),
                        RightValue = (float)Decimal.Parse(measDTO.RightValue, CultureInfo.InvariantCulture.NumberFormat),
                        LeftChange = 0,
                        RightChange = 0,
                        UserId = measDTO.UserId,
                        MuscleId=measDTO.MuscleId
                    };
                    DataBase.DoubleMeasurements.Create(meas);
                    DataBase.Save();            
            }
        }

        public void DeleteMeasurement(int id)
        {
            if (id != 0)
            {
                DataBase.DoubleMeasurements.Delete(id);
                DataBase.Save();
            }
            else
            {
                throw new ValidationException("Измерение не найдено");
            }
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public DoubleMeasurementsDTO GetMeasurement(int id)
        {
            if (id!= 0)
            {
                var meas = DataBase.DoubleMeasurements.Get(id);
                    if (meas!=null)
                    {
                    var LeftValue = meas.LeftValue.ToString();
                    return new DoubleMeasurementsDTO
                    {
                        Date = meas.Date,
                        LeftValue = LeftValue,
                        RightValue = meas.RightValue.ToString(),
                        LeftChange = meas.LeftChange,
                        RightChange = meas.RightChange,
                        UserId = meas.UserId,
                        MuscleId =meas.MuscleId
                    };
                    }
                throw new ValidationException("Измерение не найдено");
            }
            throw new ValidationException("Измерение не найдено");
        }

        public IEnumerable<DoubleMeasurementsDTO> GetMeasurements()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DoubleMeasurements, DoubleMeasurementsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<DoubleMeasurements>, List<DoubleMeasurementsDTO>>(DataBase.DoubleMeasurements.GetAll());
        }

        public IEnumerable<DoubleMeasurementsDTO> GetNeedMeasurements(User user, int type)
        {
            IEnumerable<DoubleMeasurementsDTO> needmeas = GetMeasurements().Where(x => x.UserId == user.Id).Where(y=>y.MuscleId ==type);
            return needmeas;
        }

        public void UpdateMeasurement(DoubleMeasurementsDTO measDTO, User user)
        {
            var meas = DataBase.DoubleMeasurements.Get(measDTO.ID);
            var meass = GetNeedMeasurements(user, measDTO.MuscleId);
            if (meas != null)
            {
                if (meass.Count()!=1)
                {
                    DoubleMeasurementsDTO premeas = GetPreMeasurement(meass.Reverse().Skip(1).FirstOrDefault());
                    var LeftValue = (float)Decimal.Parse(measDTO.LeftValue, CultureInfo.InvariantCulture);
                    var RightValue = (float)Decimal.Parse(measDTO.RightValue, CultureInfo.InvariantCulture);
                    var PreLeftValue = (float)Decimal.Parse(premeas.LeftValue);
                    var PreRightValue = (float)Decimal.Parse(premeas.RightValue);
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.LeftValue = LeftValue;
                    meas.RightValue = RightValue;
                    meas.MuscleId= measDTO.MuscleId;
                    meas.LeftChange = (float)Math.Round((LeftValue - PreLeftValue),4);
                    meas.RightChange = (float)Math.Round((RightValue - PreRightValue),4);
                    DataBase.DoubleMeasurements.Update(meas);
                    DataBase.Save();
                }
                else
                {
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.MuscleId = measDTO.MuscleId;
                    meas.LeftValue = (float)Decimal.Parse(measDTO.LeftValue, CultureInfo.GetCultureInfo("en-US"));
                    meas.RightValue = (float)Decimal.Parse(measDTO.RightValue, CultureInfo.GetCultureInfo("en-US"));
                    meas.LeftChange = 0;
                    meas.RightChange = 0;
                    DataBase.DoubleMeasurements.Update(meas);
                    DataBase.Save();
                }
            }
        }

        private DoubleMeasurementsDTO GetPreMeasurement(DoubleMeasurementsDTO premeas)
        {
            return new DoubleMeasurementsDTO
            {
                ID = premeas.ID,
                Date = premeas.Date,
                LeftValue = premeas.LeftValue.ToString(),
                RightValue = premeas.RightValue.ToString(),
                LeftChange = premeas.LeftChange,
                RightChange = premeas.RightChange,
                UserId = premeas.UserId,
                MuscleId =premeas.MuscleId
            };
        }


        private IEnumerable<MusclesMeasurementsDTO> GetTypeOfMuscles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MusclesMeasurements, MusclesMeasurementsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<MusclesMeasurements>, List<MusclesMeasurementsDTO>>(DataBase.Muscles.GetTypes());
        }

        public MusclesMeasurementsDTO GetTypeOfMuscle(string type)
        {
            IEnumerable <MusclesMeasurementsDTO > muscles = GetTypeOfMuscles();
            return muscles.FirstOrDefault(x => x.Name == type);
        }
    }
}
