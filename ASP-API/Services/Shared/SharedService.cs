using ASP_API.Model.Public;
using Microsoft.EntityFrameworkCore;

namespace ASP_API.Services.Shared
{
    public class SharedService: ISharedService
    {
        private readonly Context _context;
        public SharedService(Context context)
        {
            _context = context;
        }

        public void AddDomain(Domain domain)
        {
            _context.Domains.Add(domain);
            _context.SaveChanges();

        }

        public void DeleteDomain(int id)
        {
            throw new NotImplementedException();
        }

        public Domain GetDomain(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Domain> GetDomains()
        {
            return _context.Domains.ToList();
        }

        public Domain UpdateDomain(Domain domain)
        {
            throw new NotImplementedException();
        }
    }
}
