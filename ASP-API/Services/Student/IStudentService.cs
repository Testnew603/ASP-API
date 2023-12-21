using ASP_API.DTO;
using ASP_API.Model.Student;

namespace ASP_API.Services.Student
{
    public interface IStudentService
    {
        IList<StudentDetails> GetStudentDetails();
        StudentDetails GetStudent(int id);
        void Register(StudentDetails studentDetails);
        StudentBasicDetailDTO UpdateStudent(StudentBasicDetailDTO studentBasicDetailDTO);
        void DeleteStudent(int id);
        Task UpdateStudentProfile(IFormFile formFile, FileType fileType, int studentid);
    }
}