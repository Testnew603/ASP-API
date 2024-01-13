using ASP_API.DTO;
using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IFeeService
    {
        public Task<IEnumerable<Fees>> GetFeeList();
        public Task<Fees> GetFeeById(int? id);
        public Task<Fees> AddFee(Fees fees);
        public Task<Fees> UpdateFee(Fees fees);
        public Task<bool> DeleteFee(int? id);


        public Task<FeeDTO> BalanceFeePayment(FeeDTO feeDTO);
    }
}