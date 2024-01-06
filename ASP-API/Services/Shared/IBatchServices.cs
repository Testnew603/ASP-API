using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;

namespace ASP_API.Services.Shared
{
    public interface IBatchServices
    {
        public Task<IEnumerable<Batch>> GetBatchList();
        public Task<Batch> GetBatchById(int? id);
        public Task<Batch> AddBatch(Batch batch);
        public Task<Batch> UpdateBatch(Batch batch);
        public Task<bool> DeleteBatch(int? id);        
    }
}