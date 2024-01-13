using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace ASP_API.Services.Shared
{
    public class StudentAgreementServices : IStudentAgreementServices
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public StudentAgreementServices(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentAgreement>> GetStudAgreementList()
        {
            try
            {
                return await _context.StudentAgreement.ToListAsync();
            }
            catch (DbException ex) { throw ex; }
        }
        public async Task<StudentAgreement> GetStudAgreementById(int? id)
        {
            try
            {
                var studAgreement = await _context.StudentAgreement.FirstOrDefaultAsync(x => x.Id == id);
                if (studAgreement == null)
                {
                    throw new NullReferenceException("Student Agreement not found");
                }
                return studAgreement;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching Student Agreement", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching Student Agreement", ex);
            }
        }
        public async Task<StudentAgreement> AddStudAgreement(StudentAgreement studentAgreement)
        {
            var studentDomain = (from s in _context.Students
                                 where s.Id == studentAgreement.StudentId
                                 select s).FirstOrDefault();
            var studentIdExist = _context.StudentAgreement.FirstOrDefault(x => x.StudentId == studentAgreement.StudentId);
            var courseFeeDomain = (from s in _context.CourseFees
                                   where s.DomainId == studentDomain.DomainId
                                   select s).FirstOrDefault();
            var domainMatch = _context.CourseFees.Include(s => s.DomainId == studentAgreement.DomainId);
            if (courseFeeDomain == null || domainMatch == null) { throw new NullReferenceException("Domain not matching"); }
            if(studentIdExist != null) { throw new DuplicateNameException($"agreement already exist for student {studentAgreement.StudentId}"); }
            try
            {
                var newStudAgreement = new StudentAgreement
                {
                    StudentId = studentAgreement.StudentId,
                    DomainId = studentDomain.DomainId,
                    CourseFeeID = studentAgreement.CourseFeeID,
                    StartedAt = studentAgreement.StartedAt,
                    EndedAt = studentAgreement.StartedAt,
                    Documents = studentAgreement.Documents,
                };
                newStudAgreement.Status = AgreementStatus.NOTVERIFIED;
                                          
                var result = _context.StudentAgreement.Add(newStudAgreement);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return studentAgreement;
        }
        public async Task<StudentAgreement> UpdateStudAgreement(StudentAgreement studentAgreement)
        {
            var studentDomain = (from s in _context.Students
                                 where s.Id == studentAgreement.StudentId
                                 select s).FirstOrDefault();
            var courseFeeDomain = (from s in _context.CourseFees
                                   where s.DomainId == studentDomain.DomainId
                                   select s).FirstOrDefault();
            var domainMatch = _context.CourseFees.Include(s => s.DomainId == studentAgreement.DomainId);
            if (courseFeeDomain == null || domainMatch == null) { throw new NullReferenceException("Domain not matching"); }
            List<CourseFee> courseFees = _context.CourseFees.Where(c => 
                                        c.DomainId == studentAgreement.DomainId).ToList();
            try
            {
                var result = _context.StudentAgreement.FirstOrDefault(x => x.Id == studentAgreement.Id);

                result.StudentId = studentAgreement.StudentId;
                result.DomainId = studentDomain.DomainId;
                result.CourseFeeID = studentAgreement.CourseFeeID;
                result.StartedAt = studentAgreement.StartedAt;
                result.EndedAt = studentAgreement.EndedAt;
                result.Documents = studentAgreement.Documents;

                if (Enum.TryParse(Convert.ToString(studentAgreement.Status), out AgreementStatus status))
                    result.Status = status;
                else result.Status = AgreementStatus.NOTVERIFIED;

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteStudAgreement(int? id)
        {
            try
            {
                if (id == null || id == 0) { throw new ArgumentNullException(nameof(id), $"Id cannot be null or {0}"); }
                var agreementIdExist = _context.StudentAgreement.FirstOrDefault(x => x.Id == id);
                if (agreementIdExist == null) { throw new Exception($"Course Fee Id {id} not found."); }
                {
                    var result = _context.StudentAgreement.Remove(agreementIdExist);
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
