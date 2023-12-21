using ASP_API.Model.Public;
using ASP_API.Model.Student;

namespace ASP_API.Services.Shared
{
    public interface ISharedService
    {
        IList<Domain> GetDomains();
        Domain GetDomain(int id);
        void AddDomain(Domain domain);
        Domain UpdateDomain(Domain domain);
        void DeleteDomain(int id);
    }
}