﻿using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Model.Student;
using ASP_API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Dynamic.Core.Tokenizer;
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

        [HttpGet("AdminLogin")]      
        public ActionResult<ResponseMessages> AdminLogin(string email, string password)
        {
            var response = new ResponseMessages();
            var adminCredentials = new AdminCredentials
            {
                Email = "admin",
                Password = "123",
            };
            try
            {
            if(email == adminCredentials.Email && password == adminCredentials.Password)
            {
                var role = "ADMIN";
                var token = _jwtService.GenerateTokenAdmin(adminCredentials, role);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Accepted;
                    response.Result = token;                    
            }  else { throw new Exception(); }        

            } catch (Exception ex)
            {
                response.IsSuccess= false;
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
                } else { throw new Exception(); }
            } catch (Exception ex)
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

                    if (result.Status == Status.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == Status.BLOCKED)
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

                    if (result.Status == Status.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == Status.BLOCKED)
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

                    if (result.Status == AdvisorStatus.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == AdvisorStatus.BLOCKED)
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

                    if (result.Status == Status.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == Status.BLOCKED)
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

                    if (result.Status == Status.PENDING)
                    { return Ok("unapproved"); }

                    if (result.Status == Status.BLOCKED)
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
