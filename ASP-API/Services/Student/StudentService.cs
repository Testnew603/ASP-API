using ASP_API.DTO;
using ASP_API.Model.Student;

namespace ASP_API.Services.Student
{
    public class StudentService: IStudentService
    {
        private readonly Context _context;
        private readonly EmailService _emailService;
        public StudentService(Context context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }        

        public void Register(StudentDetails studentDetails)
        {
            studentDetails.Status = Status.PENDING;

            _context.Add(studentDetails);
            _context.SaveChanges();

            const string subject = "Account Created";
            var body = @"
                <html>
                    <body> 
                        <h1>Hello, " + studentDetails.FirstName + studentDetails.LastName + @"</h1>
                        <h2>
                            Your account has been created and we have sent approval request to admin. 
                            Once the request is approved by admin you will receive email, and you will be
                            able to login in to your account.
                        </h2>
                        <h3>Thank you</h3>
                    </body>
                </html>
            ";
            _emailService.SendEmail(studentDetails.Email, subject, body);
        }

        public void DeleteStudent(int id)
        {
            var result = _context.Students.FirstOrDefault(x => x.Id == id);
            if(result != null)
            {
                _context.Students.Remove(result);
                _context.SaveChanges();
            }
        }

        public StudentDetails GetStudent(int id)
        {
           return _context.Students.FirstOrDefault(x => x.Id == id)!;                      
        }

        public IList<StudentDetails> GetStudentDetails()
        {
            return _context.Students.ToList();
        }

        public StudentBasicDetailDTO UpdateStudent(StudentBasicDetailDTO studentBasicDetailDTO)
        {
            var result = _context.Students.FirstOrDefault(x => x.Id == studentBasicDetailDTO.Id);
            if(result != null)
            {
                result.FirstName = studentBasicDetailDTO.FirstName;
                result.LastName = studentBasicDetailDTO.LastName;
                result.BirthDate = studentBasicDetailDTO.BirthDate;
                result.Gender = studentBasicDetailDTO.Gender;
                result.Email = studentBasicDetailDTO.Email;
                result.Address = studentBasicDetailDTO.Address;
                result.Mobile = studentBasicDetailDTO.Mobile;
                result.Qualification = studentBasicDetailDTO.Qualification;
                result.DomainId = studentBasicDetailDTO.DomainId;
                result.Status = studentBasicDetailDTO.Status;

                _context.Students.Update(result);
                _context.SaveChanges();
            }
            return studentBasicDetailDTO;
        }

        public async Task UpdateStudentProfile(IFormFile formFile, FileType fileType, int studentid)
        {
           try
            {
                var fileDetails = new StudentProfileDTO()
                {
                    Id = studentid,
                    FileName = formFile.FileName,
                    FileType = fileType,
                };

                using (var stream = new MemoryStream())
                {
                    formFile.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                var result = _context.Students.FirstOrDefault(x => x.Id == fileDetails.Id);
                result.Profile = fileDetails.FileName;
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}
