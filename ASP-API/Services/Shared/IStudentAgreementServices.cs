using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IStudentAgreementServices
    {
        public Task<IEnumerable<StudentAgreement>> GetStudAgreementList();
        public Task<StudentAgreement> GetStudAgreementById(int? id);
        public Task<StudentAgreement> AddStudAgreement(StudentAgreement studentAgreement);
        public Task<StudentAgreement> UpdateStudAgreement(StudentAgreement studentAgreement);
        public Task<bool> DeleteStudAgreement(int? id);
    }
}