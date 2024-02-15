using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP_API.Services.Staff
{
    public class GeneralManagerService : IGeneralManagerService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public GeneralManagerService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GeneralManager>> GetGeneralManagerList()
        {
            return await _context.GeneralManager.ToListAsync();
        }
        public async Task<GeneralManager> GetGeneralManagerById(int id)
        {
            return await _context.GeneralManager.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<GeneralManager> AddGeneralManager(GeneralManager manager, IFormFile imageFile, IFormFile docFile)
        {
            try
            {
                var newGeneralManager = new GeneralManager
                {
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                    BirthDate = manager.BirthDate,
                    Gender = manager.Gender,
                    Email = manager.Email,
                    Mobile = manager.Mobile,
                    Qualification = manager.Qualification,
                    Password = manager.Password,
                };
                    newGeneralManager.Status = StaffStatus.PENDING;
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/manager/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                newGeneralManager.Profile = filename;

                string docname = Guid.NewGuid().ToString() + Path.GetExtension(docFile.FileName);
                var docpath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/manager/");
                if (!Directory.Exists(docpath))
                {
                    Directory.CreateDirectory(docpath);
                }

                using (var docstream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
                {
                    await docFile.CopyToAsync(docstream);
                }
                newGeneralManager.Documents = docname;

                var managerWithSameEmail = _context.Advisors.Where(s => s.Email == newGeneralManager.Email).ToList();
                if (managerWithSameEmail.Any())
                {
                    throw new InvalidOperationException("email already exist");
                }
                var result = _context.GeneralManager.Add(newGeneralManager);
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
        public async Task<GeneralManagerDTO> UpdateGeneralManager(GeneralManagerDTO managerDTO)
        {
            var response = new ResponseMessages();
            try
            {
                var managerExist = _context.GeneralManager.FirstOrDefault(x => x.Id == managerDTO.Id);
                if (managerExist == null) { throw new Exception("Manager not found."); }               

                _mapper.Map(managerDTO, managerExist);
                var result = _context.GeneralManager.Update(managerExist);
                await _context.SaveChangesAsync();

                return managerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
        public async Task<bool> DeleteGeneralManager(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var managerIdExist = _context.GeneralManager.FirstOrDefault(x => x.Id == id);
                if (managerIdExist == null) { throw new Exception($"Advisor Id {id} not found."); }
                var result = _context.Remove(managerIdExist);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<GeneralManagerDocumentDTO> UpdateGeneralManagerDocument(GeneralManagerDocumentDTO managerDocumentDTO)
        {
            try
            {
                string docname = Guid.NewGuid().ToString() + Path.GetExtension(managerDocumentDTO.Document.FileName);
                var docpath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/manager/");

                using (var stream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
                {
                    await managerDocumentDTO.Document.CopyToAsync(stream);
                }

                var result = _context.GeneralManager.FirstOrDefault(x => x.Id == managerDocumentDTO.Id);
                if (result == null) { throw new Exception(); }

                result.Documents = docname;
                await _context.SaveChangesAsync();
                return managerDocumentDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<GeneralManagerProfileDTO> UpdateGeneralManagerProfile(GeneralManagerProfileDTO managerProfileDTO)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(managerProfileDTO.Profile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/manager/");

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await managerProfileDTO.Profile.CopyToAsync(stream);
                }

                var result = _context.GeneralManager.FirstOrDefault(x => x.Id == managerProfileDTO.Id);
                if (result == null) { throw new Exception(); }

                result.Profile = filename;
                await _context.SaveChangesAsync();
                return managerProfileDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
