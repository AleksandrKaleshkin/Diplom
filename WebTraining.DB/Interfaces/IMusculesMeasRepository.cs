using WebTraining.DB.Models.Measurements;

namespace WebTraining.DB.Interfaces
{
    public interface IMusculesMeasRepository
    {
        IEnumerable<MusclesMeasurements> AllMuscle();
        MusclesMeasurements GetMuscle(int id);
    }
}
