using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Services.Shared;
using ASP_API.Services.Staff;
using ASP_API.Services.Student;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorController : ControllerBase
    {        
        private readonly IAdvisorService _advisorService;
        private ResponseMessages _response;

        public AdvisorController(IAdvisorService advisorService, ResponseMessages response)
        {            
            _advisorService = advisorService;
            _response = response;
        }

        [HttpGet("advisorlist")]
        public async Task<IActionResult> AdvisorList()
        {
            try
            {
                var advisorList = await _advisorService.GetAdvisorList();

                if (advisorList == null || !advisorList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = advisorList;
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

        [HttpGet("getadvisorbyid")]
        public async Task<IActionResult> GetAdvisorById(int Id)
        {
            try
            {
                var advisor = await _advisorService.GetAdvisorById(Id);
                if (advisor == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = advisor;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response.Result);

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.Unauthorized;

                return StatusCode((int)HttpStatusCode.Unauthorized, _response.ErrorMessages);
            }
        }

        [HttpPost("addadvisor")]
        public async Task<IActionResult> AddAdvisor([FromForm] Advisor advisor, IFormFile imageFile, IFormFile docFile)
        {
            try
            {
                var result = await _advisorService.AddAdvisor(advisor, imageFile, docFile);

                _response.IsSuccess = true;
                _response.Result = result;
                _response.StatusCode = HttpStatusCode.OK; 
                
                return Ok(_response.Result);
                
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode= HttpStatusCode.Unauthorized;
                _response.ErrorMessages.Add(ex.Message);

                return StatusCode((int)HttpStatusCode.Unauthorized, _response.ErrorMessages);
            }
        }

        [HttpPut("updateadvisor")]
        public async Task<IActionResult> UpdateAdvisor(AdvisorDTO advisorDTO)
        {
            try
            {
                var result = await _advisorService.UpdateAdvisor(advisorDTO);

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

        [HttpPut("updateadvisorprofile")]
        public async Task<IActionResult> UpdateAdvisorProfile([FromForm] AdvisorProfileDTO profileDTO)
        {
            try
            {
                var result = await _advisorService.UpdateAdvisorProfile(profileDTO);

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
        [HttpPut("updateadvisordocument")]
        public async Task<IActionResult> UpdateAdvisorDocument([FromForm] AdvisorDocumentDTO documentDTO)
        {
            try
            {
                var result = await _advisorService.UpdateAdvisorDocument(documentDTO);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "updated";
                return Ok(_response.Result);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Result = "file corrupted";

                return StatusCode(500, ex.ToString());              
            }
        }

        [HttpDelete("deleteadvisor")]
        public async Task<IActionResult> DeleteAdvisor(int id)
        {
            try
            {
                await _advisorService.DeleteAdvisor(id);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "deleted";

                return Ok(_response.Result);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Result = ex.ToString();

                return Ok(_response.Result);
            }
        }      
    }
}
