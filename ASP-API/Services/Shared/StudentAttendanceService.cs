using ASP_API.Model.Public;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Globalization;

namespace ASP_API.Services.Shared
{
    public class StudentAttendanceService : IStudentAttendanceService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public StudentAttendanceService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StudentAttendance>> GetStudentAttendanceList()
        {
            try
            {
                return await _context.StudentAttendance.ToListAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
        }
        public async Task<StudentAttendance> GetStudentAttendance(int id)
        {
            try
            {
                var attendance = await _context.StudentAttendance.FirstOrDefaultAsync(x => x.Id == id);
                if (attendance == null)
                {
                    throw new NullReferenceException("Attendance not found");
                }
                return attendance;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error occurred while fetching attendance", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error occurred while fetching attendance", ex);
            }
        }
        public async Task<StudentAttendance> AddStudentAttendance(StudentAttendance studentAttendance)
        {
            try
           {
                var matchingWithAdvisorStud = _context.AllocStudentToAdvisor
                    .FirstOrDefault(x => x.AdvisorId == studentAttendance.AdvisorId &&
                        x.StudentId == studentAttendance.StudentId);
                if (matchingWithAdvisorStud == null)
                    throw new NullReferenceException();

                DateTime officeEntryTime =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0);
                DateTime officeExitTime =
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 5, 30, 0);

                if(studentAttendance.EntryTime <= DateTime.Now)                
                    studentAttendance.EntryTime = DateTime.Now;

                if (studentAttendance.EntryTime > officeEntryTime)
                    studentAttendance.Status = AttendnaceStatus.LATE;
                else
                    studentAttendance.Status = AttendnaceStatus.PRESENT;

                studentAttendance.ExitTime = officeExitTime;

                var result = _context.StudentAttendance.Add(studentAttendance);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("error occured");
            }
            return studentAttendance;
        }
        public async Task<StudentAttendance> UpdateStudentAttendance(StudentAttendance studentAttendance)
        {
            try
            {
                var result = _context.StudentAttendance.FirstOrDefault(x => x.Id == studentAttendance.Id);                
                if (result == null) { throw new NullReferenceException(); }

                _mapper.Map(studentAttendance, result);

                await _context.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw new Exception("invalid id");
            }
        }
        public async Task<bool> DeleteStudentAttendance(int? id)
        {
            try
            {
                if (id == null) { throw new ArgumentNullException(nameof(id), "Id cannot be null"); }
                var attendanceIdExist = _context.StudentAttendance.FirstOrDefault(x => x.Id == id);
                if (attendanceIdExist == null) { throw new Exception($"Attendance Id {id} not found."); }
                {
                    var result = _context.StudentAttendance.Remove(attendanceIdExist);
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
