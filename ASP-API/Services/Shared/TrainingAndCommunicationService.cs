using ASP_API.Model.Public;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class TrainingAndCommunicationService : ITrainingAndCommunicationService
    {
        private readonly Context _context;

        public TrainingAndCommunicationService(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingAndCommunication>> GetTrainingScheduleList()
        {
            try
            {
                return await _context.TrainingAndCommunication.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<TrainingAndCommunication> GetTrainingScheduleById(int? id)
        {
            try
            {
                var session = await _context.TrainingAndCommunication.FirstOrDefaultAsync(x => x.Id == id);
                if (session == null)
                {
                    throw new NullReferenceException("Session not found");
                }
                return session;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching session", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching session", ex);
            }
        }
        public async Task<TrainingAndCommunication> AddTrainingSchedule(TrainingAndCommunication trainingAndCommunication)
        {
            try
            {
                var batchStudStrength = 
                    (from sc in _context.AllocBatchBranchToStudent
                    where sc.BatchId == trainingAndCommunication.BatchId
                    select sc.StudentId).Count();

                if (batchStudStrength == 0)
                    throw new NullReferenceException();

                //here the trainer id get from the token and place to the table.
                trainingAndCommunication.BatchStrength = batchStudStrength;

                var result = _context.TrainingAndCommunication.Add(trainingAndCommunication);                    
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return trainingAndCommunication;
        }
        public async Task<TrainingAndCommunication> UpdateTrainingSchedule(TrainingAndCommunication trainingAndCommunication)
        {
            try
            {
                var batchStudStrength =
                    (from sc in _context.AllocBatchBranchToStudent
                     where sc.BatchId == trainingAndCommunication.BatchId
                     select sc.StudentId).Count();

                if (batchStudStrength == 0)
                    throw new NullReferenceException();

                var result = _context.TrainingAndCommunication.FirstOrDefault(x => x.Id == trainingAndCommunication.Id);

                result.TrainerId = trainingAndCommunication.TrainerId;
                result.BatchId = trainingAndCommunication.BatchId;
                result.BatchStrength = batchStudStrength;
                result.AttendedStrength = trainingAndCommunication.AttendedStrength;
                result.Activity = trainingAndCommunication.Activity;
                result.CreatedAt = trainingAndCommunication.CreatedAt;
                result.Status = trainingAndCommunication.Status;

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteTrainingSchedule(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var sessionIdExist = _context.TrainingAndCommunication.FirstOrDefault(x => x.BatchId == id);
                if (sessionIdExist == null) { throw new Exception($"Session Id {id} not found."); }
                {
                    var result = _context.TrainingAndCommunication.Remove(sessionIdExist);
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
