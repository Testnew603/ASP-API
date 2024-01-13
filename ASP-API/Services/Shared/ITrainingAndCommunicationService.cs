using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface ITrainingAndCommunicationService
    {
        public Task<IEnumerable<TrainingAndCommunication>> GetTrainingScheduleList();
        public Task<TrainingAndCommunication> GetTrainingScheduleById(int? id);
        public Task<TrainingAndCommunication> AddTrainingSchedule(TrainingAndCommunication trainingAndCommunication);
        public Task<TrainingAndCommunication> UpdateTrainingSchedule(TrainingAndCommunication trainingAndCommunication);
        public Task<bool> DeleteTrainingSchedule(int? id);
    }
}