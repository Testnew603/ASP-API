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

        public AdminController(
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

        /// START STUDENT SECTION ///
        [HttpPost("Register")]
        public ActionResult<ResponseMessages> Register(StudentDetails studentDetails)
        {
            ResponseMessages response = new ResponseMessages();
            try
            {
                _studentService.Register(studentDetails);
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
            ResponseMessages response = new ResponseMessages();

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
                response.Result = ex.Message;
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpGet("GetStudent/{id}")]
        public ActionResult<ResponseMessages> GetStudent(int id)
        {
            ResponseMessages response = new ResponseMessages();
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
        public ActionResult<ResponseMessages> UpdateStudent(StudentBasicDetailDTO studentBasicDetailDTO)
        {
            ResponseMessages response = new ResponseMessages();
            try
            {
                var result = _studentService.UpdateStudent(studentBasicDetailDTO);
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
            ResponseMessages response = new ResponseMessages();
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
        public async Task<ActionResult<ResponseMessages>> UpdateProfile(IFormFile formFile, FileType fileType, int studentid)
        {
            ResponseMessages response = new ResponseMessages();
            if (formFile == null || fileType == null)
            {
                return BadRequest();
            }
            try
            {
                await _studentService.UpdateStudentProfile(formFile, fileType, studentid);
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


        /// END STUDENT SECTION ///

    }
}
