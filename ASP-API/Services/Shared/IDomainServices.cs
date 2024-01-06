using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IDomainServices
    {
        public Task<IEnumerable<Domain>> GetDomainList();
        public Task<Domain> GetDomainById(int? id);
        public Task<Domain> AddDomain(Domain domain);
        public Task<Domain> UpdateDomain(Domain domain);
        public Task<bool> DeleteDomain(int? id);
    }
}