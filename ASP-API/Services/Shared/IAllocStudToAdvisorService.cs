using ASP_API.DTO;
using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IAllocStudToAdvisorService
    {
        public Task<IEnumerable<AllocStudentToAdvisor>> GetAllocStudToAdvisorList();
        public Task<AllocStudentToAdvisor> GetAllocStudToAdvisorById(int? id);
        public Task<AllocStudentToAdvisorDTO> AllocStudToAdvisor(AllocStudentToAdvisorDTO allocStudentToAdvisorDTO);
        public Task<AllocStudentToAdvisorDTO> UpdateAllocStudToAdvisor(AllocStudentToAdvisorDTO studentToAdvisorDTO);
        public Task<bool> DeleteAllocStudToAdvisor(int? id);
    }
}