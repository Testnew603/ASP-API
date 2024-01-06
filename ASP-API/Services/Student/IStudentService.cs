using ASP_API.DTO;
using ASP_API.Model.Student;

namespace ASP_API.Services.Student
{
    public interface IStudentService
    {
        IList<StudentDetails> GetStudentDetails();
        StudentDetails GetStudent(int id);
        Task RegisterAsync(StudentDetails studentDetails, IFormFile imageFile, IFormFile docFile);
        StudentUpdateDTO UpdateStudent(StudentUpdateDTO studentDTO);
        void DeleteStudent(int id);
        Task UpdateStudentProfile(IFormFile formFile, int studentid);
    }
}