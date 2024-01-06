using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class CondonationFeeService : ICondonationFeeService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public CondonationFeeService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CondonationFees>> GetCondonationFeeList()
        {
            try
            {
                return await _context.CondonationFees.ToListAsync();
            } catch (DbException ex) { throw ex; }            
        }

        public async Task<CondonationFees> GetCondonationFeeById(int? id)
        {
            try
            {
                var condonationFee = await _context.CondonationFees.FirstOrDefaultAsync(x => x.Id == id);
                if (condonationFee == null)
                {
                    throw new NullReferenceException("Condonation fee not found");
                }
                return condonationFee;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching Condonation fee", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching Condonation fee", ex);
            }
        }

        public async Task<CondonationFees> AddCondonationFee(CondonationFees condonationFees)
        {
            try
            {
                var newCondonationFee = new CondonationFees
                {
                    StudentId = condonationFees.StudentId,
                    FineType = condonationFees.FineType,
                    FineAmount = condonationFees.FineAmount,
                    Reason = condonationFees.Reason,                    
                    CreatedAt = ($"{DateTime.Now:dd-MM-yyyy}"),
                };
                newCondonationFee.Status = FineStatus.NOTPAID;

                var result = _context.CondonationFees.Add(newCondonationFee);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return condonationFees;
        }

        public async Task<CondonationFees> UpdateCondonationFee(CondonationFees condonationFees)
        {
            try
            {
                var result = _context.CondonationFees.FirstOrDefault(x => x.Id == condonationFees.Id);

                _mapper.Map(condonationFees, result);

                result.CreatedAt = ($"{DateTime.Now:dd-MM-yyyy}");
                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }

        public async Task<bool> DeleteCondonationFee(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var condonationFeeIdExist = _context.CondonationFees.FirstOrDefault(x => x.Id == id);
                if (condonationFeeIdExist == null) { throw new Exception($"Course Fee Id {id} not found."); }
                {
                    var result = _context.CondonationFees.Remove(condonationFeeIdExist);
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
