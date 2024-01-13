using ASP_API.DTO;
using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class AllocStudToAdvisorService : IAllocStudToAdvisorService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public AllocStudToAdvisorService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AllocStudentToAdvisor>> GetAllocStudToAdvisorList()
        {
            try
            {
                return await _context.AllocStudentToAdvisor.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<AllocStudentToAdvisor> GetAllocStudToAdvisorById(int? id)
        {
            try
            {
                var domain = await _context.AllocStudentToAdvisor.FirstOrDefaultAsync(x => x.Id == id);
                if (domain == null)
                {
                    throw new NullReferenceException("Id not found");
                }
                return domain;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching details", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching details", ex);
            }
        }
        public async Task<AllocStudentToAdvisorDTO> AllocStudToAdvisor(AllocStudentToAdvisorDTO allocStudentToAdvisorDTO)
        {
            try
            {
                var domainMatching =
                    from s in _context.Students.Where(x => x.Id == allocStudentToAdvisorDTO.StudentId)
                    join a in _context.Advisors.Where(x => x.Id == allocStudentToAdvisorDTO.AdvisorId)
                    on s.DomainId equals a.DomainId
                    select s.DomainId;

                var studExist = _context.AllocStudentToAdvisor
                    .Where(x => x.StudentId == allocStudentToAdvisorDTO.StudentId).FirstOrDefault();

                var batchIdmatching = 
                    from sb in _context.AllocBatchBranchToStudent.Where(x => x.StudentId == allocStudentToAdvisorDTO.StudentId)
                    join ab in _context.AllocBatchToAdvisors.Where(x => x.AdvisorId == allocStudentToAdvisorDTO.AdvisorId)
                    on sb.BatchId equals ab.BatchId
                    select sb.BatchId;

                if(studExist != null) { throw new InvalidDataException(); }

                AllocStudentToAdvisor newAllocation = new AllocStudentToAdvisor
                {
                    StudentId = allocStudentToAdvisorDTO.StudentId,
                    AdvisorId = allocStudentToAdvisorDTO.AdvisorId,                    
                };
                newAllocation.DomainId = domainMatching.FirstOrDefault();
                newAllocation.BatchId = batchIdmatching.FirstOrDefault();
                                
                var result = _context.AllocStudentToAdvisor.Add(newAllocation);
                await _context.SaveChangesAsync();                
                return allocStudentToAdvisorDTO;
            }
            catch
            {
                throw new Exception("error occured");
            }                        
        }
        public async Task<AllocStudentToAdvisorDTO> UpdateAllocStudToAdvisor(AllocStudentToAdvisorDTO allocStudentToAdvisorDTO)
        {
            try
            {
                var result = _context.AllocStudentToAdvisor.FirstOrDefault(x => x.Id == allocStudentToAdvisorDTO.Id);
                if(result == null) { throw new KeyNotFoundException(); }

                var domainMatching =
                    from s in _context.Students.Where(x => x.Id == allocStudentToAdvisorDTO.StudentId)
                    join a in _context.Advisors.Where(x => x.Id == allocStudentToAdvisorDTO.AdvisorId)
                    on s.DomainId equals a.DomainId
                    select s.DomainId;

                var studExist = _context.AllocStudentToAdvisor
                    .Where(x => x.StudentId == allocStudentToAdvisorDTO.StudentId).FirstOrDefault();

                var batchIdmatching =
                    from sb in _context.AllocBatchBranchToStudent.Where(x => x.StudentId == allocStudentToAdvisorDTO.StudentId)
                    join ab in _context.AllocBatchToAdvisors.Where(x => x.AdvisorId == allocStudentToAdvisorDTO.AdvisorId)
                    on sb.BatchId equals ab.BatchId
                    select sb.BatchId;

                if (studExist != null && studExist.Id != allocStudentToAdvisorDTO.Id) { throw new InvalidDataException(); }

                result.StudentId = allocStudentToAdvisorDTO.StudentId;
                result.AdvisorId = allocStudentToAdvisorDTO.AdvisorId;                
                result.DomainId = domainMatching.FirstOrDefault();
                result.BatchId = batchIdmatching.FirstOrDefault();
                result.CreatedAt = allocStudentToAdvisorDTO.CreatedAt;
                result.Status = allocStudentToAdvisorDTO.Status;

                _context.AllocStudentToAdvisor.Update(result);
                await _context.SaveChangesAsync();
                return allocStudentToAdvisorDTO;                                               
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteAllocStudToAdvisor(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var allocIdExist = _context.AllocStudentToAdvisor.FirstOrDefault(x => x.Id == id);
                if (allocIdExist == null) { throw new Exception($"Alloc Id {id} not found."); }
                {
                    var result = _context.AllocStudentToAdvisor.Remove(allocIdExist);
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
