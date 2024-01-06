using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP_API.Services.Staff
{
    public class ReviwerService : IReviwerService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ReviwerService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Reviewer>> GetReviewerList()
        {
            return await _context.Reviewer.ToListAsync();
        }
        public async Task<Reviewer> GetReviewerById (int? id)
        {
            try
            {
                if(id == null) { throw new NullReferenceException(nameof(id)); }
                return await _context.Reviewer.FirstOrDefaultAsync(x => x.Id == id);
            } catch (Exception ex) { throw new OperationCanceledException(ex.ToString()); }
        }
        public async Task<Reviewer> AddReviewer(Reviewer reviewer, IFormFile imageFile)
        {
            try
            {
                var newReviewer = new Reviewer
                {
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName,                    
                    Gender = reviewer.Gender,
                    Email = reviewer.Email,
                    Mobile = reviewer.Mobile,                    
                    Password = reviewer.Password,
                    DomainId = reviewer.DomainId,
                };
                newReviewer.Status = Status.PENDING;

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/reviewer/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                newReviewer.Profile = filename;               

                var advisorWithSameEmail = _context.Advisors.Where(s => s.Email == newReviewer.Email).ToList();
                if (advisorWithSameEmail.Any())
                {
                    throw new InvalidOperationException("email already exist");
                }
                var result = _context.Reviewer.Add(newReviewer);
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
        public async Task<ReviewerDTO> UpdateReviewer(ReviewerDTO reviewerDTO)
        {
            var response = new ResponseMessages();
            try
            {
                var reviewerExist = _context.Reviewer.FirstOrDefault(x => x.Id == reviewerDTO.Id);
                if (reviewerExist == null) { throw new NullReferenceException(); }

                reviewerExist.FirstName = reviewerDTO.FirstName;
                reviewerExist.LastName = reviewerDTO.LastName;                
                reviewerExist.Gender = reviewerDTO.Gender;
                reviewerExist.Email = reviewerDTO.Email;
                reviewerExist.Mobile = reviewerDTO.Mobile;                
                reviewerExist.DomainId = reviewerDTO.DomainId;
                reviewerExist.Status = reviewerDTO.Status;
                
                var result = _context.Reviewer.Update(reviewerExist);
                await _context.SaveChangesAsync();

                return reviewerDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                throw;
            }
        }
        public async Task<bool> DeleteReviewer(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var reviewerIdExist = _context.Reviewer.FirstOrDefault(x => x.Id == id);
                if (reviewerIdExist == null) { throw new Exception($"Reviewer Id {id} not found."); }
                var result = _context.Remove(reviewerIdExist);
                await _context.SaveChangesAsync();
                return result != null ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<ReviwerProfileDTO> UpdateReviewerProfile(ReviwerProfileDTO profileDTO)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(profileDTO.Profile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles/reviewer/");

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await profileDTO.Profile.CopyToAsync(stream);
                }

                var result = _context.Reviewer.FirstOrDefault(x => x.Id == profileDTO.Id);
                if (result == null) { throw new NullReferenceException(); }

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
