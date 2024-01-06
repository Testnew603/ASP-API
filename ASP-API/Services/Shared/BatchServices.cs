using ASP_API.Model.Public;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{    
    public class BatchServices : IBatchServices
    {
        private readonly Context _context;
        public BatchServices(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Batch>> GetBatchList()
        {
            try
            {
                return await _context.Batches.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<Batch> GetBatchById(int? id)
        {
            try
            {
                var batch = await _context.Batches.FirstOrDefaultAsync(x => x.BatchId == id);
                if (batch == null)
                {
                    throw new NullReferenceException("Batch not found");
                }
                return batch;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching batch", ex);
            }
            catch (Exception ex)
            {                
                throw new Exception("Unexpected error occurred while fetching batch", ex);
            }
        }
        public async Task<Batch> AddBatch(Batch batch)
        {
            try
            {
                var result = _context.Batches.Add(batch);
                await _context.SaveChangesAsync();                
            }
            catch
            {
                throw new Exception("error occured");                
            }
            return batch;
        }
        public async Task<Batch> UpdateBatch(Batch batch)
        {
            try
            {
                var result = _context.Batches.FirstOrDefault(x => x.BatchId == batch.BatchId);
                
                result.BatchName = batch.BatchName;
                result.StartedAt = batch.StartedAt;

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteBatch(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var batchIdExist = _context.Batches.FirstOrDefault(x => x.BatchId == id);
                if (batchIdExist == null) { throw new Exception($"Batch Id {id} not found."); }
                {
                    var result = _context.Batches.Remove(batchIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            } catch (Exception ex)
            {
               throw new Exception(ex.ToString());
            }
        }
    }
}
