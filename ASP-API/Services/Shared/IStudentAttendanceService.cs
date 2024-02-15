using ASP_API.DTO;
using ASP_API.Model.Public;

namespace ASP_API.Services.Shared
{
    public interface IStudentAttendanceService
    {
        public Task<IEnumerable<StudentAttendance>> GetStudentAttendanceList();
        public Task<StudentAttendance> GetStudentAttendance(int id);
        public Task<StudAttendanceEntryDTO> AddStudentAttendanceEntry(StudAttendanceEntryDTO attendanceEntryDTO);
        public Task<StudAttendanceExitDTO> AddStudentAttendanceExit(StudAttendanceExitDTO attendanceExitDTO);
        public Task<StudentAttendance> UpdateStudentAttendance(StudentAttendance studentAttendance);
        public Task<bool> DeleteStudentAttendance(int? id);
        public Task<string> GetLocalIpAddress();
    }
}