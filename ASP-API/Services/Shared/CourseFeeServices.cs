using ASP_API.Model.Public;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class CourseFeeServices : ICourseFeeServices
    {
        private readonly Context _context;

        public CourseFeeServices(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseFee>> GetCourseFeeList()
        {
            try
            {
                return await _context.CourseFees.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<CourseFee> GetCourseFeeById(int? id)
        {
            try
            {
                var courseFee = await _context.CourseFees.FirstOrDefaultAsync(x => x.Id == id);
                if (courseFee == null)
                {
                    throw new NullReferenceException("Course fee not found");
                }
                return courseFee;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching Course fee", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching Course fee", ex);
            }
        }
        public async Task<CourseFee> AddCourseFee(CourseFee courseFee)
        {
            try
            {
                var newCourseFee = new CourseFee
                {
                    DomainId = courseFee.DomainId,
                    FeeAmount = courseFee.FeeAmount,
                    Tax = double.Parse(courseFee.FeeAmount) * 18 / 100,
                    CreatedAt = ($"{DateTime.Now:dd-MM-yyyy}"),
                };
                var result = _context.CourseFees.Add(newCourseFee);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return courseFee;
        }
        public async Task<CourseFee> UpdateCourseFee(CourseFee courseFee)
        {
            try
            {
                var result = _context.CourseFees.FirstOrDefault(x => x.Id == courseFee.Id);

                result.DomainId = courseFee.DomainId;
                result.FeeAmount = courseFee.FeeAmount;
                result.Tax = double.Parse(courseFee.FeeAmount) * 18 / 100;
                result.CreatedAt = ($"{DateTime.Now:dd-MM-yyyy}");
                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteCourseFee(int? id)
        {
            try
            {
                if (id == null || id == 0) { throw new ArgumentNullException(nameof(id), $"Id cannot be null or {0}"); }
                var courseFeeIdExist = _context.CourseFees.FirstOrDefault(x => x.Id == id);
                if (courseFeeIdExist == null) { throw new Exception($"Course Fee Id {id} not found."); }
                {
                    var result = _context.CourseFees.Remove(courseFeeIdExist);
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
