using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP_API.Services.Staff
{
    public class AdvisorService : IAdvisorService
    {
        private Context _context;
        private readonly IMapper _mapper;

        public AdvisorService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Advisor>> GetAdvisorList()
        {
            return await _context.Advisors.ToListAsync();
        }
        public async Task<Advisor> GetAdvisorById(int id)
        {
            return await _context.Advisors.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Advisor> AddAdvisor(Advisor advisor, IFormFile imageFile, IFormFile docFile)
        {
            try
            {                     
            var newAdvisor = new Advisor
            {
                FirstName = advisor.FirstName,
                LastName = advisor.LastName,
                BirthDate = advisor.BirthDate,
                Gender = advisor.Gender,
                Email = advisor.Email,
                Mobile = advisor.Mobile,
                Qualification = advisor.Qualification,
                Password = advisor.Password,
                DomainId = advisor.DomainId,
            };
                newAdvisor.Status = StaffStatus.PENDING;

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/advisor/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
                newAdvisor.Profile = filename;

            string docname = Guid.NewGuid().ToString() + Path.GetExtension(docFile.FileName);
            var docpath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/advisor/");
            if (!Directory.Exists(docpath))
            {
                Directory.CreateDirectory(docpath);
            }

            using (var docstream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
            {
                await docFile.CopyToAsync(docstream);
            }
                newAdvisor.Documents = docname;

            var advisorWithSameEmail = _context.Advisors.Where(s => s.Email == newAdvisor.Email).ToList();
            if (advisorWithSameEmail.Any())
            {
                throw new InvalidOperationException("email already exist");
            }
            var result = _context.Advisors.Add(newAdvisor);
            await _context.SaveChangesAsync();
            return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.Message);
                Console.WriteLine("Inner Exception: "+ex.InnerException?.Message);
                throw;                
            }
        }
        public async Task<AdvisorDTO> UpdateAdvisor(AdvisorDTO advisorDTO)
        {
            var response = new ResponseMessages();
            try
            {
                var advisorExist = _context.Advisors.FirstOrDefault(x => x.Id == advisorDTO.Id);
                if(advisorExist == null) { throw new Exception(); }

                advisorExist.FirstName = advisorDTO.FirstName;
                advisorExist.LastName = advisorDTO.LastName;
                advisorExist.BirthDate = advisorDTO.BirthDate;
                advisorExist.Gender = advisorDTO.Gender;
                advisorExist.Email = advisorDTO.Email;
                advisorExist.Mobile = advisorDTO.Mobile;
                advisorExist.Qualification = advisorDTO.Qualification;
                advisorExist.DomainId = advisorDTO.DomainId;
                advisorExist.Status = advisorDTO.Status;
                
                _mapper.Map(advisorDTO, advisorExist);
                var result = _context.Advisors.Update(advisorExist);
                await _context.SaveChangesAsync();
               
                return advisorDTO;
            } catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }                        
        }

        public async Task<AdvisorStatusUpdateDTO> UpdateAdvisorStatus(AdvisorStatusUpdateDTO statusUpdateDTO)
        {
            var response = new ResponseMessages();
            try
            {
                var advisorExist = _context.Advisors.FirstOrDefault(x => x.Id == statusUpdateDTO.Id);
                if (advisorExist == null) { throw new Exception(); }

                advisorExist.Status = statusUpdateDTO.Status;
                _mapper.Map(statusUpdateDTO, advisorExist);
                var result = _context.Advisors.Update(advisorExist);
                await _context.SaveChangesAsync();

                return statusUpdateDTO;

            } catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }

        }

        public async Task<bool> DeleteAdvisor(int? id)
        {
            try
            {
                if(id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var advisorIdExist = _context.Advisors.FirstOrDefault(x => x.Id == id);
                if(advisorIdExist == null) { throw new Exception($"Advisor Id {id} not found."); }
                var result = _context.Remove(advisorIdExist);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            } catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }          
        }
        public async Task<AdvisorProfileDTO> UpdateAdvisorProfile(AdvisorProfileDTO profileDTO)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(profileDTO.Profile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/advisor/");

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await profileDTO.Profile.CopyToAsync(stream);
                }             

                var result = _context.Advisors.FirstOrDefault(x => x.Id == profileDTO.Id);
                if (result == null) { throw new Exception(); }

                result.Profile = filename;
                await _context.SaveChangesAsync();
                return profileDTO;
            } catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task<AdvisorDocumentDTO> UpdateAdvisorDocument(AdvisorDocumentDTO documentDTO)
        {
            try
            {
                string docname = Guid.NewGuid().ToString() + Path.GetExtension(documentDTO.Document.FileName);
                var docpath = Path.Combine(Directory.GetCurrentDirectory(), "Documents/advisor/");

                using (var stream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
                {
                    await documentDTO.Document.CopyToAsync(stream);
                }

                var result = _context.Advisors.FirstOrDefault(x => x.Id == documentDTO.Id);
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

    }
}
