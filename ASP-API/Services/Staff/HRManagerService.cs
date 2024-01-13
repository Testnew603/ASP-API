using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using Microsoft.EntityFrameworkCore;

namespace ASP_API.Services.Staff
{
    public class HRManagerService : IHRManagerService
    {
        private readonly Context _context;

        public HRManagerService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HRManager>> GetHRManagerList()
        {
            return await _context.HRManager.ToListAsync();
        }
        public async Task<HRManager> GetHRManagerById(int id)
        {
            return await _context.HRManager.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<HRManager> AddHRManager(HRManager hrManager, IFormFile imageFile, IFormFile docFile)
        {
            try
            {
                var newHRManager = new HRManager
                {
                    FirstName = hrManager.FirstName,
                    LastName = hrManager.LastName,
                    BirthDate = hrManager.BirthDate,
                    Gender = hrManager.Gender,
                    Email = hrManager.Email,
                    Mobile = hrManager.Mobile,
                    Qualification = hrManager.Qualification,
                    Password = hrManager.Password,                    
                };
                newHRManager.Status = Status.PENDING;

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/hrmanager/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                newHRManager.Profile = filename;

                string docname = Guid.NewGuid().ToString() + Path.GetExtension(docFile.FileName);
                var docpath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/hrmanager/");
                if (!Directory.Exists(docpath))
                {
                    Directory.CreateDirectory(docpath);
                }

                using (var docstream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
                {
                    await docFile.CopyToAsync(docstream);
                }
                newHRManager.Documents = docname;

                var advisorWithSameEmail = _context.HRManager.Where(s => s.Email == newHRManager.Email).ToList();
                if (advisorWithSameEmail.Any())
                {
                    throw new InvalidOperationException("email already exist");
                }
                var result = _context.HRManager.Add(newHRManager);
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
        public async Task<HRManagerDTO> UpdateHRManager(HRManagerDTO hrManagerDTO)
        {
            
            try
            {
                var hrManagerExist = _context.HRManager.FirstOrDefault(x => x.Id == hrManagerDTO.Id);
                if (hrManagerExist == null) { throw new Exception(); }

                hrManagerExist.FirstName = hrManagerDTO.FirstName;
                hrManagerExist.LastName = hrManagerDTO.LastName;
                hrManagerExist.BirthDate = hrManagerDTO.BirthDate;
                hrManagerExist.Gender = hrManagerDTO.Gender;
                hrManagerExist.Email = hrManagerDTO.Email;
                hrManagerExist.Mobile = hrManagerDTO.Mobile;
                hrManagerExist.Qualification = hrManagerDTO.Qualification;
                hrManagerExist.Status = hrManagerDTO.Status;
                
                var result = _context.HRManager.Update(hrManagerExist);
                await _context.SaveChangesAsync();

                return hrManagerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
        public async Task<bool> DeleteHRManager(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var hrManagerIdExist = _context.HRManager.FirstOrDefault(x => x.Id == id);
                if (hrManagerIdExist == null) { throw new Exception($"HRManager Id {id} not found."); }
                var result = _context.Remove(hrManagerIdExist);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<HRManagerDocumentDTO> UpdateHRManagerDocument(HRManagerDocumentDTO documentDTO)
        {
            try
            {
                string docname = Guid.NewGuid().ToString() + Path.GetExtension(documentDTO.Document.FileName);
                var docpath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/hrmanager/");

                using (var stream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
                {
                    await documentDTO.Document.CopyToAsync(stream);
                }

                var result = _context.HRManager.FirstOrDefault(x => x.Id == documentDTO.Id);
                if (result == null) { throw new Exception(); }

                result.Documents = docname;
                await _context.SaveChangesAsync();
                return documentDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }    
        public async Task<HRManagerProfileDTO> UpdateHRManagerProfile(HRManagerProfileDTO profileDTO)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(profileDTO.Profile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/hrmanager/");

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await profileDTO.Profile.CopyToAsync(stream);
                }

                var result = _context.HRManager.FirstOrDefault(x => x.Id == profileDTO.Id);
                if (result == null) { throw new NullReferenceException(); }

                result.Profile = filename;
                await _context.SaveChangesAsync();
                return profileDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
