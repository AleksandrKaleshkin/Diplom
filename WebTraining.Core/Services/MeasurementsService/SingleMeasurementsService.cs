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
        private readonly ISingleMeasRepository<SingleMeasurements> singleMeasRepository;    
        private readonly IMapper mapper;
        private readonly IMusculesMeasRepository musculesRepository;

        public SingleMeasurementsService(ISingleMeasRepository<SingleMeasurements> singleMeasRepository, IMapper mapper, IMusculesMeasRepository musculesRepository)
        {
            this.singleMeasRepository = singleMeasRepository;
            this.musculesRepository = musculesRepository;
            this.mapper = mapper;
        }

        public void AddMeasurement(SingleMeasurementstDTO measDTO, User user)
        {
            var meass = GetNeedMeasurements(user, measDTO.MuscleId).OrderBy(x => x.Date);
            if (meass.Count() > 0)
            {
                var premeas = meass.Last();
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
                        TypeOfMuscle=musculesRepository.GetMuscle(measDTO.MuscleId)
                    };
                    singleMeasRepository.Create(meas);
                    singleMeasRepository.Save();
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
                    TypeOfMuscle = musculesRepository.GetMuscle(measDTO.MuscleId)
                };
                singleMeasRepository.Create(meas);
                singleMeasRepository.Save();
            }
        }

        public void DeleteMeasurement(int id)
        {
            if (id != 0)
            {
                singleMeasRepository.Delete(id);
                singleMeasRepository.Save();
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
                var meas = singleMeasRepository.Get(id);
                if (meas != null)
                {
                    return mapper.Map<SingleMeasurementstDTO>(meas);
                }
                throw new ValidationException("Измерение не найдено");
            }
            throw new ValidationException("Измерение не найдено");
        }

        public IEnumerable<SingleMeasurementstDTO> GetMeasurements()
        {
            var measurements_list = singleMeasRepository.GetAll();
            return mapper.Map<IEnumerable<SingleMeasurementstDTO>>(measurements_list);
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
            var meas = singleMeasRepository.Get(measDTO.ID);
            var meass = GetNeedMeasurements(user, measDTO.MuscleId);
            if (meas != null)
            {
                if (meass.Count() != 1)
                {
                    SingleMeasurementstDTO premeas = meass.Reverse().Skip(1).FirstOrDefault();
                    meas.Date = measDTO.Date;
                    meas.Value = measDTO.Value;
                    meas.Change = (float)Math.Round((measDTO.Value - premeas.Value), 4);
                    meas.MuscleId = measDTO.MuscleId;
                    singleMeasRepository.Update(meas);
                    singleMeasRepository.Save();
                }
                else
                {
                    meas.Date = measDTO.Date.ToUniversalTime();
                    meas.Value = measDTO.Value;
                    meas.Change = 0;
                    meas.MuscleId = measDTO.MuscleId;                   
                    singleMeasRepository.Update(meas);
                    singleMeasRepository.Save();
                }
            }
        }

        private IEnumerable<MusclesMeasurementsDTO> GetTypeOfMuscles()
        {
            var type_list = musculesRepository.AllMuscle();
            return mapper.Map<IEnumerable<MusclesMeasurementsDTO>>(type_list);
        }

        public MusclesMeasurementsDTO GetTypeOfMuscle(string type)
        {
            return GetTypeOfMuscles().FirstOrDefault(x => x.Name == type);
        }
    }
}
