using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Student;
using ASP_API.Services.Shared;
using ASP_API.Services.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly Context _context;
        private readonly JwtService _jwtService;
        private readonly ISharedService _sharedService;
        private readonly IStudentService _studentService;
        private readonly IWebHostEnvironment _environment;
        private ResponseMessages response;

        public AdminController(
            Context context,
            EmailService emailService,
            JwtService jwtService,
            ISharedService sharedService,
            IStudentService studentService,
            IWebHostEnvironment environment
            )
        {
            _context = context;
            _jwtService = jwtService;
            _sharedService = sharedService;
            _studentService = studentService;
            _environment = environment ?? throw new ArgumentNullException( nameof( environment ) );
            response = new();
        }

        /////////////////////////////////////////////// START STUDENT SECTION ///////////////////////////////////////////
        
        [HttpPost("StudentRegister")]
        public async Task<ResponseMessages> Register([FromForm]StudentDetails studentDetails, IFormFile imageFile, IFormFile docFile)
        {   
            var response = new ResponseMessages();
            try
            {                      
                    await _studentService.RegisterAsync(studentDetails, imageFile, docFile);
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Result = "Your account has been sent for approval. Once it is approved you will get an email.";
               
            } catch (Exception ex)
            {                
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.Result = ex.Message;
            }

            return response;
        }

        [HttpGet("StudentList")]
        public ActionResult<ResponseMessages> StudentList()
        {
            try
            {
                var result = _studentService.GetStudentDetails().ToList();

                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = result;
                return Ok(result);
            } catch(Exception ex)
            {
                response.StatusCode=HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("GetStudent/{id}")]
        public ActionResult<ResponseMessages> GetStudent(int id)
        {
           
            try
            {
                var result = _studentService.GetStudent(id);
                if (result != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Result = result;
                } else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.Result = "id not available";
                }
            }
            catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.IsSuccess = false;
                response.Result = ex.Message;
            }

            return Ok(response);
        }                     

        [HttpPut("UpdateStudent")]
        public ActionResult<ResponseMessages> UpdateStudent(StudentUpdateDTO studentDTO)
        {
            
            try
            {
                var result = _studentService.UpdateStudent(studentDTO);
                if(result != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Result = result;
                } else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.Result = "id not matching";
                }                          
            } catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.Result = ex.Message;
            }
            return response;
        }

        [HttpDelete("RemoveStudent")]
        public ActionResult<ResponseMessages> RemoveStudent(int id)
        {   
            
            try
            {
                var result = _context.Students.FirstOrDefault(x => x.Id == id);
                if (result != null)
                {
                    _studentService.DeleteStudent(id);
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Result = "student id:"+ id + " removed";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    response.Result = "id not matching";
                }
            } catch(Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.Result = ex.Message;
            }
            return response;            
        }

        [HttpPut("UpdateProfile")]
        public async Task<ActionResult<ResponseMessages>> UpdateProfile(IFormFile formFile, int studentid)
        {
           
            if (formFile == null)
            {
                return BadRequest();
            }
            try
            {
                await _studentService.UpdateStudentProfile(formFile, studentid);
                response.StatusCode = HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Result = "updated";
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.Result = ex.Message;
                throw;
            }
        }


        //////////////////////////////////////////////// END STUDENT SECTION ////////////////////////////////////////////                   
    }
}
