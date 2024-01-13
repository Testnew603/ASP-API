using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocStudentToAdvisorController : ControllerBase
    {
        private readonly IAllocStudToAdvisorService _allocStudToAdvisorService;
        private readonly ResponseMessages _response;

        public AllocStudentToAdvisorController(IAllocStudToAdvisorService allocStudToAdvisorService,
            ResponseMessages response)
        {
            _allocStudToAdvisorService = allocStudToAdvisorService;
            _response = response;
        }

        [HttpGet("allocStudToAdvisorList")]
        public async Task<IActionResult> AllocStudToAdvisorList()
        {
            try
            {
                var allocatedList = await _allocStudToAdvisorService.GetAllocStudToAdvisorList();

                if (allocatedList == null || !allocatedList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = allocatedList;
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

        [HttpGet("getAllocStudToAdvisorById")]
        public async Task<IActionResult> GetAllocStudToAdvisorById(int Id)
        {
            try
            {
                var allocStudent = await _allocStudToAdvisorService.GetAllocStudToAdvisorById(Id);
                if (allocStudent == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = allocStudent;
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

        [HttpPost("allocStudToAdvisor")]
        public async Task<IActionResult> AllocStudToAdvisor(AllocStudentToAdvisorDTO allocStudentToAdvisorDTO)
        {
            try
            {
                var result = await _allocStudToAdvisorService.AllocStudToAdvisor(allocStudentToAdvisorDTO);

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

        [HttpPut("updateAllocStudToAdvisor")]
        public async Task<IActionResult> UpdateAllocStudToAdvisor(AllocStudentToAdvisorDTO allocStudentToAdvisorDTO)
        {
            try
            {
                var result = await _allocStudToAdvisorService.UpdateAllocStudToAdvisor(allocStudentToAdvisorDTO);

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

        [HttpDelete("deleteAllocStudToAdvisor")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            try
            {
                await _allocStudToAdvisorService.DeleteAllocStudToAdvisor(id);
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
