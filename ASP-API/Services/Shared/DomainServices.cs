using ASP_API.Model.Public;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Net.NetworkInformation;

namespace ASP_API.Services.Shared
{
    public class DomainServices : IDomainServices
    {
        private readonly Context _context;
        public DomainServices(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain>> GetDomainList()
        {
            try
            {
                return await _context.Domains.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<Domain> GetDomainById(int? id)
        {
            try
            {
                var domain = await _context.Domains.FirstOrDefaultAsync(x => x.Id == id);
                if (domain == null)
                {
                    throw new NullReferenceException("Domain not found");
                }
                return domain;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching domain", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching domain", ex);
            }
        }
        public async Task<Domain> AddDomain(Domain domain)
        {
            try
            {
                var result = _context.Domains.Add(domain);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return domain;
        }
        public async Task<Domain> UpdateDomain(Domain domain)
        {
            try
            {
                var result = _context.Domains.FirstOrDefault(x => x.Id == domain.Id);

                result.MainDomain = domain.MainDomain;
                result.SubDomain = domain.SubDomain;

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteDomain(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var domainIdExist = _context.Domains.FirstOrDefault(x => x.Id == id);
                if (domainIdExist == null) { throw new Exception($"Domain Id {id} not found."); }
                {
                    var result = _context.Domains.Remove(domainIdExist);
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
