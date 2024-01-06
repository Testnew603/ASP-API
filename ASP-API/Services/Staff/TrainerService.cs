using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using Microsoft.EntityFrameworkCore;

namespace ASP_API.Services.Staff
{
    public class TrainerService : ITrainerService
    {
        private readonly Context _context;

        public TrainerService(Context context)
        {
            _context = context;
        }
        public async Task<Trainer> AddTrainer(Trainer trainer, IFormFile imageFile)
        {
            try
            {
                var newTrainer = new Trainer
                {
                    FirstName = trainer.FirstName,
                    LastName = trainer.LastName,                    
                    Gender = trainer.Gender,
                    Email = trainer.Email,
                    Mobile = trainer.Mobile,                    
                    Password = trainer.Password, 
                    SpecializedIn = trainer.SpecializedIn,
                };
                newTrainer.Status = Status.PENDING;

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/trainer/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                newTrainer.Profile = filename;                

                var trainerWithSameEmail = _context.Trainer.Where(s => s.Email == newTrainer.Email).ToList();
                if (trainerWithSameEmail.Any())
                {
                    throw new InvalidOperationException("email already exist");
                }
                var result = _context.Trainer.Add(newTrainer);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                throw;
            }
        }

        public async Task<bool> DeleteTrainer(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var trainerIdExist = _context.Trainer.FirstOrDefault(x => x.Id == id);
                if (trainerIdExist == null) { throw new Exception($"Trainer Id {id} not found."); }
                var result = _context.Remove(trainerIdExist);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Trainer> GetTrainerById(int id)
        {
            return await _context.Trainer.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Trainer>> GetTrainerList()
        {
            return await _context.Trainer.ToListAsync();
        }

        public async Task<TrainerDTO> UpdateTrainer(TrainerDTO trainerDTO)
        {
            var response = new ResponseMessages();
            try
            {
                var TrainerExist = _context.Trainer.FirstOrDefault(x => x.Id == trainerDTO.Id);
                if (TrainerExist == null) { throw new Exception(); }

                TrainerExist.FirstName = trainerDTO.FirstName;
                TrainerExist.LastName = trainerDTO.LastName;                
                TrainerExist.Gender = trainerDTO.Gender;
                TrainerExist.Email = trainerDTO.Email;
                TrainerExist.Mobile = trainerDTO.Mobile;
                TrainerExist.SpecializedIn = trainerDTO.SpecializedIn;
                TrainerExist.Status = trainerDTO.Status;
                
                var result = _context.Trainer.Update(TrainerExist);
                await _context.SaveChangesAsync();

                return trainerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }

        public async Task<TrainerProfileDTO> UpdateTrainerProfile(TrainerProfileDTO profileDTO)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(profileDTO.Profile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/trainer/");

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await profileDTO.Profile.CopyToAsync(stream);
                }

                var result = _context.Trainer.FirstOrDefault(x => x.Id == profileDTO.Id);
                if (result == null) { throw new Exception(); }

                result.Profile = filename;
                await _context.SaveChangesAsync();
                return profileDTO;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
