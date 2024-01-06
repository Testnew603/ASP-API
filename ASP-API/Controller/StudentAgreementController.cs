using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAgreementController : ControllerBase
    {
        private readonly IStudentAgreementServices _studentAgreement;
        private readonly ResponseMessages _response;

        public StudentAgreementController(IStudentAgreementServices studentAgreement, ResponseMessages response)
        {
            _studentAgreement = studentAgreement;
            _response = response;
        }

        [HttpGet("studagreementlist")]
        public async Task<IActionResult> StudAgreementList()
        {
            try
            {
                var studAgreementList = await _studentAgreement.GetStudAgreementList();

                if (studAgreementList == null || !studAgreementList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = studAgreementList;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response.Result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.Unauthorized;

                return StatusCode((int)HttpStatusCode.Unauthorized, _response.ErrorMessages);
            }
        }

        [HttpGet("getstudagreementbyid")]
        public async Task<IActionResult> GetStudAgreementById(int Id)
        {
            try
            {
                var studentAgreement = await _studentAgreement.GetStudAgreementById(Id);
                if (studentAgreement == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = studentAgreement;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response.Result);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.Unauthorized;

                return StatusCode((int)HttpStatusCode.Unauthorized, _response.ErrorMessages);
            }
        }

        [HttpPost("addstudagreement")]
        public async Task<IActionResult> AddStudAgreement(StudentAgreement studentAgreement)
        {
            try
            {
                var result = await _studentAgreement.AddStudAgreement(studentAgreement);

                _response.IsSuccess = true;
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response.Result);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add(ex.Message);

                return StatusCode((int)HttpStatusCode.Unauthorized, _response.ErrorMessages);
            }
        }

        [HttpPut("updatestudagreement")]
        public async Task<IActionResult> UpdateStudAgreement(StudentAgreement studentAgreement)
        {
            try
            {
                var result = await _studentAgreement.UpdateStudAgreement(studentAgreement);

                _response.IsSuccess = true;
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response.Result);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add(ex.Message);

                return StatusCode((int)HttpStatusCode.Unauthorized, _response.ErrorMessages);
            }
        }

        [HttpDelete("deletestudagreement")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                await _studentAgreement.DeleteStudAgreement(id);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "deleted";

                return Ok(_response.Result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Result = ex.ToString();

                return Ok(_response.Result);
            }
        }
    }
}
