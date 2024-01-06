using ASP_API.Model.Public;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class BranchServices : IBranchServices
    {
        private readonly Context _context;
        public BranchServices(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetBranchList()
        {
            try
            {
                return await _context.Branches.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<Branch> GetBranchById(int? id)
        {
            try
            {
                var branch = await _context.Branches.FirstOrDefaultAsync(x => x.BranchId == id);
                if (branch == null)
                {
                    throw new NullReferenceException("Branch not found");
                }
                return branch;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching branch", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching branch", ex);
            }
        }
        public async Task<Branch> AddBranch(Branch branch)
        {
            try
            {
                var result = _context.Branches.Add(branch);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return branch;
        }
        public async Task<Branch> UpdateBranch(Branch branch)
        {
            try
            {
                var result = _context.Branches.FirstOrDefault(x => x.BranchId == branch.BranchId);

                result.BranchName = branch.BranchName;
                result.Status = branch.Status;

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteBranch(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var branchIdExist = _context.Branches.FirstOrDefault(x => x.BranchId == id);
                if (branchIdExist == null) { throw new Exception($"Branch Id {id} not found."); }
                {
                    var result = _context.Branches.Remove(branchIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
