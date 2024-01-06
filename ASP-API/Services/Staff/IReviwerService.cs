using ASP_API.DTO;
using ASP_API.Model.Staff;

namespace ASP_API.Services.Staff
{
    public interface IReviwerService
    {
        public Task<IEnumerable<Reviewer>> GetReviewerList();
        public Task<Reviewer> GetReviewerById(int? id);
        public Task<Reviewer> AddReviewer(Reviewer reviewer, IFormFile imageFile);
        public Task<ReviewerDTO> UpdateReviewer(ReviewerDTO reviewerDTO);
        public Task<bool> DeleteReviewer(int? id);
        public Task<ReviwerProfileDTO> UpdateReviewerProfile(ReviwerProfileDTO profileDTO);        
    }
}