using ASP_API.DTO;
using ASP_API.Model.Staff;

namespace ASP_API.Services.Staff
{
    public interface ITrainerService
    {
        public Task<IEnumerable<Trainer>> GetTrainerList();
        public Task<Trainer> GetTrainerById(int id);
        public Task<Trainer> AddTrainer(Trainer trainer, IFormFile imageFile);
        public Task<TrainerDTO> UpdateTrainer(TrainerDTO trainerDTO);
        public Task<bool> DeleteTrainer(int? id);
        public Task<TrainerProfileDTO> UpdateTrainerProfile(TrainerProfileDTO profileDTO);      
    }
}