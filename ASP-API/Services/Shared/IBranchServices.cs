using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IBranchServices
    {
        public Task<IEnumerable<Branch>> GetBranchList();
        public Task<Branch> GetBranchById(int? id);
        public Task<Branch> AddBranch(Branch branch);
        public Task<Branch> UpdateBranch(Branch branch);
        public Task<bool> DeleteBranch(int? id);
    }
}