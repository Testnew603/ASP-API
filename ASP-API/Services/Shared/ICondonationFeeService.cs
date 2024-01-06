using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface ICondonationFeeService
    {
        public Task<IEnumerable<CondonationFees>> GetCondonationFeeList();
        public Task<CondonationFees> GetCondonationFeeById(int? id);
        public Task<CondonationFees> AddCondonationFee(CondonationFees condonationFees);
        public Task<CondonationFees> UpdateCondonationFee(CondonationFees condonationFees);
        public Task<bool> DeleteCondonationFee(int? id);
    }
}