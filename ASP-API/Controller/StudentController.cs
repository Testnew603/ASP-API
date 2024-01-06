using ASP_API.DTO;
using ASP_API.Model.Student;
using ASP_API.Services.Shared;
using ASP_API.Services.Student;
using Microsoft.AspNetCore.Mvc;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly Context _context;
        private readonly JwtService _jwtService;
        private readonly ISharedService _sharedService;
        private readonly IStudentService _studentService;
        public StudentController(
            Context context,
            EmailService emailService,
            JwtService jwtService,
            ISharedService sharedService,
            IStudentService studentService
            )
        {
            _context = context;
            _jwtService = jwtService;
            _sharedService = sharedService;
            _studentService = studentService;
        }
        
        //public EmailService EmailService { get; }
        //public JwtService JwtService { get; }

        [HttpGet("Login")]
        public ActionResult Login(string email, string password)
        {
            if(_context.Students.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
            {
                var result = _context.Students.Single(student => student.Email.Equals(email) && student.Password.Equals(password));
                if(result.Status == Status.PENDING)
                {
                    return Ok("unapproved");
                }

                if (result.Status == Status.BLOCKED)
                {
                    return Ok("blocked");
                }

                var role = "STUDENT";
                return Ok(_jwtService.GenerateToken(result, role));

            }
            return Ok("not found");
        }
    }
}
