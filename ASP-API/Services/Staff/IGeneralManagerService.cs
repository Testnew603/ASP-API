using ASP_API.DTO;
using ASP_API.Model.Staff;

namespace ASP_API.Services.Staff
{
    public interface IGeneralManagerService
    {
        public Task<IEnumerable<GeneralManager>> GetGeneralManagerList();
        public Task<GeneralManager> GetGeneralManagerById(int id);
        public Task<GeneralManager> AddGeneralManager(GeneralManager manager, IFormFile imageFile, IFormFile docFile);
        public Task<GeneralManagerDTO> UpdateGeneralManager(GeneralManagerDTO managerDTO);
        public Task<bool> DeleteGeneralManager(int? id);
        public Task<GeneralManagerProfileDTO> UpdateGeneralManagerProfile(GeneralManagerProfileDTO managerProfileDTO);
        public Task<GeneralManagerDocumentDTO> UpdateGeneralManagerDocument(GeneralManagerDocumentDTO managerDocumentDTO);
    }
}