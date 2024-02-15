using ASP_API.DTO;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;

namespace ASP_API.Services.Staff
{
    public interface IAdvisorService
    {
        public Task <IEnumerable< Advisor>> GetAdvisorList();
        public Task <Advisor> GetAdvisorById(int id);
        public Task <Advisor> AddAdvisor(Advisor advisor, IFormFile imageFile, IFormFile docFile);
        public Task<AdvisorDTO> UpdateAdvisor(AdvisorDTO advisorDTO);
        public Task <bool> DeleteAdvisor(int? id);        
        public Task<AdvisorProfileDTO> UpdateAdvisorProfile(AdvisorProfileDTO profileDTO);
        public Task<AdvisorDocumentDTO> UpdateAdvisorDocument(AdvisorDocumentDTO documentDTO);
        public Task<AdvisorStatusUpdateDTO> UpdateAdvisorStatus(AdvisorStatusUpdateDTO statusUpdateDTO);
    }
}