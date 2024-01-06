using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseFeeController : ControllerBase
    {
        private readonly ICourseFeeServices _courseFeeServices;
        private ResponseMessages _response;

        public CourseFeeController(ICourseFeeServices courseFeeServices, ResponseMessages response)
        {
            _courseFeeServices = courseFeeServices;
            _response = response;
        }

        [HttpGet("coursefeelist")]
        public async Task<IActionResult> CourseFeeList()
        {
            try
            {
                var courseFee = await _courseFeeServices.GetCourseFeeList();

                if (courseFee == null || !courseFee.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = courseFee;
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
        
        [HttpGet("getcoursefeebyid")]
        public async Task<IActionResult> GetCourseFeeById(int Id)
        {
            try
            {
                var batch = await _courseFeeServices.GetCourseFeeById(Id);
                if (batch == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = batch;
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

        [HttpPost("addcoursefee")]
        public async Task<IActionResult> AddCourseFee(CourseFee courseFee)
        {
            try
            {
                var result = await _courseFeeServices.AddCourseFee(courseFee);

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

        [HttpPut("updatecoursefee")]
        public async Task<IActionResult> UpdateCourseFee(CourseFee courseFee)
        {
            try
            {
                var result = await _courseFeeServices.UpdateCourseFee(courseFee);

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

        [HttpDelete("deletecoursefee")]
        public async Task<IActionResult> DeleteCourseFee(int id)
        {
            try
            {
                await _courseFeeServices.DeleteCourseFee(id);
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
