using ASP_API.DTO;
using ASP_API.Model.Staff;

namespace ASP_API.Services.Staff
{
    public interface IHRManagerService
    {
        public Task<IEnumerable<HRManager>> GetHRManagerList();
        public Task<HRManager> GetHRManagerById(int id);
        public Task<HRManager> AddHRManager(HRManager hrManager, IFormFile imageFile, IFormFile docFile);
        public Task<HRManagerDTO> UpdateHRManager(HRManagerDTO hrManagerDTO);
        public Task<bool> DeleteHRManager(int? id);
        public Task<HRManagerProfileDTO> UpdateHRManagerProfile(HRManagerProfileDTO profileDTO);
        public Task<HRManagerDocumentDTO> UpdateHRManagerDocument(HRManagerDocumentDTO documentDTO);
    }
}