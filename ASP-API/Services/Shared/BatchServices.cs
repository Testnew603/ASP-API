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

        //Batch allocation to advisor crud
        public async Task<IEnumerable<AllocBatchToAdvisor>> GetAllocBatchToAdvisorList()
        {
            try
            {
                return await _context.AllocBatchToAdvisors.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        public async Task<AllocBatchToAdvisor> GetAllocBatchToAdvisorById(int? id)
        {
            try
            {
                var advisorBatch = await _context.AllocBatchToAdvisors.FirstOrDefaultAsync(x => x.Id == id);
                if (advisorBatch == null)
                {
                    throw new NullReferenceException("Batch not found");
                }
                return advisorBatch;
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

        public async Task<AllocBatchToAdvisor> AllocBatchToAdvisor(AllocBatchToAdvisor allocBatchToAdvisor)
        {
            try
            {
                var allocationExist = _context.AllocBatchToAdvisors
                    .Where(x => x.AdvisorId == allocBatchToAdvisor.AdvisorId)
                    .Where(x => x.BatchId == allocBatchToAdvisor.BatchId).ToList().FirstOrDefault();
                if (allocationExist != null) { throw new InvalidOperationException(); }

                var result = _context.AllocBatchToAdvisors.Add(allocBatchToAdvisor);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return allocBatchToAdvisor;
        }

        public async Task<AllocBatchToAdvisor> UpdateAllocBatchToAdvisor(AllocBatchToAdvisor allocBatchToAdvisor)
        {
            try
            {
                var result = _context.AllocBatchToAdvisors.FirstOrDefault(x => x.Id == allocBatchToAdvisor.Id);

                result.AdvisorId = allocBatchToAdvisor.AdvisorId;
                result.BatchId = allocBatchToAdvisor.BatchId;

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }

        public async Task<bool> DeleteAllocBatchToAdvisor(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var allocBatchToAdvisorIdExist = _context.AllocBatchToAdvisors.FirstOrDefault(x => x.Id == id);
                if (allocBatchToAdvisorIdExist == null) { throw new Exception($"Allocation Id {id} not found."); }
                {
                    var result = _context.AllocBatchToAdvisors.Remove(allocBatchToAdvisorIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        //Batch & Branch allocation to student crud
        public async Task<IEnumerable<AllocBatchBranchToStudent>> GetAllocBatchToStudList()
        {
            try
            {
                return await _context.AllocBatchBranchToStudent.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        public async Task<AllocBatchBranchToStudent> GetAllocBatchToStudById(int? id)
        {
            try
            {
                var studentBatch = await _context.AllocBatchBranchToStudent.FirstOrDefaultAsync(x => x.Id == id);
                if (studentBatch == null)
                {
                    throw new NullReferenceException("Allocation not found");
                }
                return studentBatch;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching allocation", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching allocation", ex);
            }
        }

        public async Task<AllocBatchBranchToStudent> AllocBatchToStud(AllocBatchBranchToStudent allocBatchBranchToStudent)
        {
            try
            {
                var allocationExist = _context.AllocBatchBranchToStudent
                    .Where(x => x.StudentId == allocBatchBranchToStudent.StudentId).FirstOrDefault();
                    
                if (allocationExist != null) { throw new InvalidOperationException(); }

                var result = _context.AllocBatchBranchToStudent.Add(allocBatchBranchToStudent);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return allocBatchBranchToStudent;
        }

        public async Task<AllocBatchBranchToStudent> UpdateAllocBatchToStud(AllocBatchBranchToStudent allocBatchBranchToStudent)
        {
            try
            {
                var allocationExist = _context.AllocBatchBranchToStudent
                    .Where(x => x.StudentId == allocBatchBranchToStudent.StudentId).FirstOrDefault();

                if (allocationExist != null && allocationExist.Id != allocBatchBranchToStudent.Id)
                 throw new InvalidOperationException();

                var result = _context.AllocBatchBranchToStudent.FirstOrDefault(x => x.Id == allocBatchBranchToStudent.Id);

                result.StudentId = allocBatchBranchToStudent.StudentId;
                result.BatchId = allocBatchBranchToStudent.BatchId;
                result.BranchId = allocBatchBranchToStudent.BranchId;
                result.CreatedAt = allocBatchBranchToStudent.CreatedAt;
                result.Status = allocBatchBranchToStudent.Status;

                _context.AllocBatchBranchToStudent.Update(result);
                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }

        public async Task<bool> DeleteAllocBatchToStud(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var allocIdExist = _context.AllocBatchBranchToStudent.FirstOrDefault(x => x.BatchId == id);
                if (allocIdExist == null) { throw new Exception($"Alloc Id {id} not found."); }
                {
                    var result = _context.AllocBatchBranchToStudent.Remove(allocIdExist);
                    await _context.SaveChangesAsync();
                    return result != null ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        //Batch allocation to trainers crud
        public async Task<IEnumerable<AllocBatchToTrainer>> GetAllocBatchToTrainerList()
        {
            try
            {
                return await _context.AllocBatchToTrainers.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }

        public async Task<AllocBatchToTrainer> GetAllocBatchToTrainerById(int? id)
        {
            try
            {
                var trainerStudBatch = await _context.AllocBatchToTrainers.FirstOrDefaultAsync(x => x.Id == id);
                if (trainerStudBatch == null)
                {
                    throw new NullReferenceException("Allocation not found");
                }
                return trainerStudBatch;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching allocation", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching allocation", ex);
            }
        }

        public async Task<AllocBatchToTrainer> AllocBatchToTrainer(AllocBatchToTrainer allocBatchToTrainer)
        {
            try
            {
                var allocationExist = _context.AllocBatchBranchToStudent
                    .Where(x => x.StudentId == allocBatchToTrainer.StudentId)
                    .Select(x => new { x.StudentId })
                    .FirstOrDefault();
     
                var refCount = _context.AllocBatchToTrainers
                    .Where(x => x.BatchRef == allocBatchToTrainer.BatchRef).Count();                

                if (allocationExist != null) { throw new InvalidOperationException(); }
                if(refCount > 20) 
                { throw new InvalidOperationException($"Batch {allocBatchToTrainer.BatchRef} is full."); }

                var result = _context.AllocBatchToTrainers.Add(allocBatchToTrainer);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return allocBatchToTrainer;
        }

        public async Task<AllocBatchToTrainer> UpdateAllocBatchToTrainer(AllocBatchToTrainer allocBatchToTrainer)
        {
            try
            {
                var allocationExist = _context.AllocBatchBranchToStudent
                    .Where(x => x.StudentId == allocBatchToTrainer.StudentId).FirstOrDefault();

                var refCount = _context.AllocBatchToTrainers
                    .Where(x => x.BatchRef == allocBatchToTrainer.BatchRef).Count();

                if (allocationExist != null && allocationExist.Id != allocBatchToTrainer.Id)
                    throw new InvalidOperationException();

                if (refCount > 20)
                { throw new InvalidOperationException($"Batch {allocBatchToTrainer.BatchRef} is full."); }

                var result = _context.AllocBatchToTrainers.FirstOrDefault(x => x.Id == allocBatchToTrainer.Id);

                result.StudentId = allocBatchToTrainer.StudentId;
                result.TrainerId = allocBatchToTrainer.TrainerId;
                result.BatchRef = allocBatchToTrainer.BatchRef;
                result.CreatedAt = allocBatchToTrainer.CreatedAt;
                result.Status = allocBatchToTrainer.Status;

                _context.AllocBatchToTrainers.Update(result);
                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }

        public async Task<bool> DeleteAllocBatchToTrainer(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var allocIdExist = _context.AllocBatchToTrainers.FirstOrDefault(x => x.Id == id);
                if (allocIdExist == null) { throw new Exception($"Alloc Id {id} not found."); }
                {
                    var result = _context.AllocBatchToTrainers.Remove(allocIdExist);
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
