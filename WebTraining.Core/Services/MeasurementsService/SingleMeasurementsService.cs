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
    public class SingleMeasurementsService : ISingleMeasurementstService
    {
        IUnitOfWork DataBase { get; set; }

        public SingleMeasurementsService(IUnitOfWork DataBase)
        {
            this.DataBase = DataBase;
        }

        public void AddMeasurement(SingleMeasurementstDTO measDTO, User user)
        {
            var meass = GetNeedMeasurements(user, measDTO.MuscleId).OrderBy(x => x.Date);
            if (meass.Count() > 0)
            {
                var premeas = GetPreMeasurement(meass.Last());
                var Value = (float)Decimal.Parse(measDTO.Value, CultureInfo.InvariantCulture);
                var PreValue = (float)Decimal.Parse(premeas.Value);
                if (premeas != null)
                {
                    SingleMeasurements meas = new SingleMeasurements
                    {
                        Date = measDTO.Date.ToUniversalTime(),
                        Value = (float)Value,
                        Change = (float)Math.Round((Value - PreValue), 4),
                        UserId = measDTO.UserId,
                        MuscleId=measDTO.MuscleId,
                        TypeOfMuscle=DataBase.Muscles.Get(measDTO.MuscleId)

                    };
                    DataBase.SingleMeasurements.Create(meas);
                    DataBase.Save();
                }
            }
            else
            {
                SingleMeasurements meas = new SingleMeasurements
                {
                    Date = measDTO.Date.ToUniversalTime(),
                    Value = (float)Decimal.Parse(measDTO.Value, CultureInfo.InvariantCulture.NumberFormat),
                    Change = 0,
                    UserId = measDTO.UserId,
                    MuscleId = measDTO.MuscleId,
                    TypeOfMuscle = DataBase.Muscles.Get(measDTO.MuscleId)
                };
                DataBase.SingleMeasurements.Create(meas);
                DataBase.Save();
            }
        }

        public void DeleteMeasurement(int id)
        {
            if (id != 0)
            {
                DataBase.SingleMeasurements.Delete(id);
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

        public SingleMeasurementstDTO GetMeasurement(int id)
        {
            if (id != 0)
            {
                var meas = DataBase.SingleMeasurements.Get(id);
                if (meas != null)
                {

                    return new SingleMeasurementstDTO
                    {
                        Date = meas.Date,
                        Value = meas.Value.ToString(),
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
            return mapper.Map<IEnumerable<SingleMeasurements>, List<SingleMeasurementstDTO>>(DataBase.SingleMeasurements.GetAll());
        }

        public IEnumerable<SingleMeasurementstDTO> GetNeedMeasurements(User user, int type)
        {
            IEnumerable<SingleMeasurementstDTO> needmeas = GetMeasurements().Where(y=>y.MuscleId == type).Where(x => x.UserId == user.Id);
            return needmeas;
        }

        public void UpdateMeasurement(SingleMeasurementstDTO measDTO, User user)
        {
            var meas = DataBase.SingleMeasurements.Get(measDTO.ID);
            var meass = GetNeedMeasurements(user, measDTO.MuscleId);
            if (meas != null)
            {
                if (meass.Count() != 1)
                {
                    SingleMeasurementstDTO premeas = GetPreMeasurement(meass.Reverse().Skip(1).FirstOrDefault());
                    var Value = (float)Decimal.Parse(measDTO.Value, CultureInfo.InvariantCulture);
                    var PreValue = (float)Decimal.Parse(premeas.Value);
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.Value = Value;
                    meas.Change = (float)Math.Round((Value - PreValue), 4);
                    meas.MuscleId = measDTO.MuscleId;
                    DataBase.SingleMeasurements.Update(meas);
                    DataBase.Save();
                }
                else
                {
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.Value = (float)Decimal.Parse(measDTO.Value, CultureInfo.GetCultureInfo("en-US"));
                    meas.Change = 0;
                    meas.MuscleId = measDTO.MuscleId;
                    DataBase.SingleMeasurements.Update(meas);
                    DataBase.Save();
                }
            }
        }

        private SingleMeasurementstDTO GetPreMeasurement(SingleMeasurementstDTO premeas)
        {
            return new SingleMeasurementstDTO
            {
                ID = premeas.ID,
                Date = premeas.Date,
                Value = premeas.Value.ToString(),
                Change = premeas.Change,
                UserId = premeas.UserId,
            };
        }

        private IEnumerable<MusclesMeasurementsDTO> GetTypeOfMuscles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MusclesMeasurements, MusclesMeasurementsDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<MusclesMeasurements>, List<MusclesMeasurementsDTO>>(DataBase.Muscles.GetTypes());
        }

        public MusclesMeasurementsDTO GetTypeOfMuscle(string type)
        {
            IEnumerable<MusclesMeasurementsDTO> muscles = GetTypeOfMuscles();
            return muscles.FirstOrDefault(x => x.Name == type);
        }
    }
}
