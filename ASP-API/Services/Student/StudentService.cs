using ASP_API.DTO;
using ASP_API.Model.Student;

namespace ASP_API.Services.Student
{
    public class StudentService: IStudentService
    {
        private readonly Context _context;
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _environment;
        public StudentService(Context context, EmailService emailService, IWebHostEnvironment environment)
        {
            _context = context;
            _emailService = emailService;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task RegisterAsync(StudentDetails studentDetails, IFormFile imageFile, IFormFile docFile)
        {
            var student = new StudentDetails
            {
                FirstName = studentDetails.FirstName,
                LastName = studentDetails.LastName,
                BirthDate = studentDetails.BirthDate,
                Gender = studentDetails.Gender,
                Email = studentDetails.Email,
                Address = studentDetails.Address,
                Mobile = studentDetails.Mobile,
                Qualification = studentDetails.Qualification,
                Documents = studentDetails.Documents,
                DomainId = studentDetails.DomainId,
                Password = studentDetails.Password
            };
                student.Status = Status.PENDING;

                string filename = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                student.Profile = filename;

            string docname = Guid.NewGuid().ToString() + Path.GetExtension(docFile.FileName);
            var docpath = Path.Combine(Directory.GetCurrentDirectory(), "documents/");
            if (!Directory.Exists(docpath))
            {
                Directory.CreateDirectory(docpath);
            }

            using (var docstream = new FileStream(Path.Combine(docpath, docname), FileMode.Create))
            {
                await imageFile.CopyToAsync(docstream);
            }
            student.Documents = docname;

            var studentsWithSameEmail = _context.Students.Where(s => s.Email ==  studentDetails.Email).ToList();
            if(studentsWithSameEmail.Any())
            {
                throw new InvalidOperationException("email already exist");
            }

            _context.Add(student);
                await _context.SaveChangesAsync();

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
            throw new NullReferenceException();
        }

        public StudentDetails GetStudent(int id)
        {
           return _context.Students.FirstOrDefault(x => x.Id == id)!;                      
        }

        public IList<StudentDetails> GetStudentDetails()
        {
            return _context.Students.ToList();
        }

        public StudentUpdateDTO UpdateStudent(StudentUpdateDTO studentDTO)
        {
            var result = _context.Students.FirstOrDefault(x => x.Id == studentDTO.Id);
            if(result != null)
            {
                result.FirstName = studentDTO.FirstName;
                result.LastName = studentDTO.LastName;
                result.BirthDate = studentDTO.BirthDate;
                result.Gender = studentDTO.Gender;
                result.Email = studentDTO.Email;
                result.Address = studentDTO.Address;
                result.Mobile = studentDTO.Mobile;
                result.Qualification = studentDTO.Qualification;
                result.DomainId = studentDTO.DomainId;
                result.Status = studentDTO.Status;

                _context.Students.Update(result);
                _context.SaveChanges();
            }
            return studentDTO;
        }

        public async Task UpdateStudentProfile(IFormFile profileImage, int studentid)
        {
           try
            {
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(profileImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images/");

                using (var stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                var fileDetails = new StudentProfileDTO()
                {
                    Id = studentid,
                    FileName = filename
                };                     

                var result = _context.Students.FirstOrDefault(x => x.Id == fileDetails.Id);
                result.Profile = fileDetails.FileName;
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw new Exception("file error");
            }
        }
    }
}
