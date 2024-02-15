using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;
        private readonly JwtService _jwtService;
        private ILogger<LoginController> _logger;

        public LoginController(Context context, JwtService jwtService, ILogger<LoginController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }

        //[HttpGet("AllLoginUser")]
        //public ActionResult<ResponseMessages> AllLoginUser(string email, string password)
        //{
        //    var response = new ResponseMessages();            

        //    var student = _context.Students
        //        .Select(x => new AllUser { Email = x.Email, Password = x.Password, Status = x.Status.ToString(), Role = "STUDENT" })
        //        .Where(x => x.Email == email && x.Password == password);
        //    var advisor = _context.Advisors
        //        .Select(x => new AllUser { Email = x.Email, Password = x.Password, Status = x.Status.ToString(), Role = "ADVISOR" })
        //        .Where(x => x.Email == email && x.Password == password);
        //    var hrmanager = _context.HRManager
        //        .Select(x => new AllUser { Email = x.Email, Password = x.Password, Status = x.Status.ToString(), Role = "HRMANAGER" })
        //        .Where(x => x.Email == email && x.Password == password);
        //    var trainer = _context.Trainer
        //        .Select(x => new AllUser { Email = x.Email, Password = x.Password, Status = x.Status.ToString(), Role = "TRAINER" })
        //        .Where(x => x.Email == email && x.Password == password);
        //    var reviewer = _context.Reviewer
        //        .Select(x => new AllUser { Email = x.Email, Password = x.Password, Status = x.Status.ToString(), Role = "REVIEWER" })
        //        .Where(x => x.Email == email && x.Password == password);
        //    List<AllUser> admin = new List<AllUser>();
        //    admin.Add(new AllUser { Email = "admin", Password = "123", Status = "ACTIVE", Role = "ADMIN" });

            
        //    var alldata = student.ToList().Union(advisor.ToList())
        //        .Union(hrmanager.ToList()).Union(trainer.ToList()).Union(reviewer.ToList()).Union(admin);

        //    string tokens = null;
        //    var token = tokens;

        //    try
        //    {
        //        if (alldata != null)
        //        {
        //            var newresult = alldata.Single(x => x.Email.Equals(email) && x.Password.Equals(password));
        //            if (newresult.Status == "PENDING")
        //            { return Ok("unapproved"); }

        //            if (newresult.Status == "BLOCKED")
        //            { return Ok("blocked"); }

        //            if(newresult.Role == "STUDENT")
        //            {
        //                var result = _context.Students.Single(u => u.Email.Equals(email) && u.Password.Equals(password));
        //                token = _jwtService.GenerateToken(result, newresult.Role);
        //            } else if(newresult.Role == "ADVISOR")
        //            {
        //                var result = _context.Advisors.Single(u => u.Email.Equals(email) && u.Password.Equals(password));
        //                token = _jwtService.GenerateToken(result, newresult.Role);
        //            } else if(newresult.Role == "HRMANAGER")
        //            {
        //                var result = _context.HRManager.Single(u => u.Email.Equals(email) && u.Password.Equals(password));
        //                token = _jwtService.GenerateToken(result, newresult.Role);
        //            } else if(newresult.Role == "TRAINER")
        //            {
        //                var result = _context.Trainer.Single(u => u.Email.Equals(email) && u.Password.Equals(password));
        //                token = _jwtService.GenerateToken(result, newresult.Role);
        //            } else if(newresult.Role == "REVIEWER")
        //            {
        //                var result = _context.Reviewer.Single(u => u.Email.Equals(email) && u.Password.Equals(password));
        //                token = _jwtService.GenerateToken(result, newresult.Role);
        //            } else
        //            {
        //                //token = _jwtService.GenerateTokenAdmin(newresult);
        //            }
                    
        //            response.IsSuccess = true;
        //            response.StatusCode = HttpStatusCode.Accepted;
        //            response.Result = token;
        //        }
        //        else { throw new Exception(); }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.StatusCode = HttpStatusCode.BadRequest;
        //        response.Result = "Invalid email or password" + ex;
        //    }
        //    return response;
        //}

        [HttpGet("AdminLogin")]
        public ActionResult<ResponseMessages> AdminLogin(string email, string password)
        {
            var response = new ResponseMessages();
            var adminCredentials = new AdminCredentials
            {
                Email = "admin",
                Password = "123",
                Role = "ADMIN",
                Status = "ACTIVE",
            };
            try
            {
                if (email == adminCredentials.Email && password == adminCredentials.Password)
                {                    
                    var token = _jwtService.GenerateTokenAdmin(adminCredentials);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }

        [HttpGet("StudentLogin")]
        public ActionResult<ResponseMessages> StudentLogin(string email, string password)
        {
            var response = new ResponseMessages();
            try
            {
                if (_context.Students.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
                {
                    var result = _context.Students.Single(student => student.Email.Equals(email) && student.Password.Equals(password));
                    if (result.Status == Status.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == Status.BLOCKED)
                    { return Ok("blocked"); }

                    var role = "STUDENT";
                    var token = _jwtService.GenerateToken(result, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }

        [HttpGet("GeneralManagerLogin")]
        public ActionResult<ResponseMessages> GeneralManagerLogin(string email, string password)
        {
            var response = new ResponseMessages();
            try
            {
                if (_context.GeneralManager.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
                {
                    var result = _context.GeneralManager.Single(student =>
                    student.Email.Equals(email) && student.Password.Equals(password));

                    if (result.Status == StaffStatus.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == StaffStatus.BLOCKED)
                    { return Ok("blocked"); }

                    var role = "GENERALMANAGER";
                    var token = _jwtService.GenerateToken(result, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }

        [HttpGet("HRManagerLogin")]
        public ActionResult<ResponseMessages> HRManagerLogin(string email, string password)
        {
            var response = new ResponseMessages();
            try
            {
                if (_context.HRManager.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
                {
                    var result = _context.HRManager.Single(student =>
                    student.Email.Equals(email) && student.Password.Equals(password));

                    if (result.Status == StaffStatus.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == StaffStatus.BLOCKED)
                    { return Ok("blocked"); }

                    var role = "HRMANAGER";
                    var token = _jwtService.GenerateToken(result, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }

        [HttpGet("AdvisorLogin")]
        public ActionResult<ResponseMessages> AdvisorLogin(string email, string password)
        {
            var response = new ResponseMessages();
            try
            {
                if (_context.Advisors.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
                {
                    var result = _context.Advisors.Single(student =>
                    student.Email.Equals(email) && student.Password.Equals(password));

                    if (result.Status == StaffStatus.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == StaffStatus.BLOCKED)
                    { return Ok("blocked"); }

                    var role = "ADVISOR";
                    var token = _jwtService.GenerateToken(result, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }

        [HttpGet("ReviewerLogin")]
        public ActionResult<ResponseMessages> ReviewerLogin(string email, string password)
        {
            var response = new ResponseMessages();
            try
            {
                if (_context.Reviewer.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
                {
                    var result = _context.Reviewer.Single(student =>
                    student.Email.Equals(email) && student.Password.Equals(password));

                    if (result.Status == StaffStatus.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == StaffStatus.BLOCKED)
                    { return Ok("blocked"); }

                    var role = "REVIEWER";
                    var token = _jwtService.GenerateToken(result, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }

        [HttpGet("TrainerLogin")]
        public ActionResult<ResponseMessages> TrainerLogin(string email, string password)
        {
            var response = new ResponseMessages();
            try
            {
                if (_context.Trainer.Any(u => u.Email.Equals(email) && u.Password.Equals(password)))
                {
                    var result = _context.Trainer.Single(student =>
                    student.Email.Equals(email) && student.Password.Equals(password));

                    if (result.Status == StaffStatus.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == StaffStatus.BLOCKED)
                    { return Ok("blocked"); }

                    var role = "TRAINER";
                    var token = _jwtService.GenerateToken(result, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = "Invalid email or password" + ex;
            }
            return response;
        }
    }
}
