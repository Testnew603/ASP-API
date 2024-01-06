using ASP_API.DTO;
using ASP_API.Migrations;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Services.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRManagerController : ControllerBase
    {
        private readonly IHRManagerService _hrManagerService;
        private readonly ResponseMessages _response;

        public HRManagerController(IHRManagerService hrManagerService, ResponseMessages response)
        {
            _hrManagerService = hrManagerService;
            _response = response;
        }

        [HttpGet("hrlist")]
        public async Task<IActionResult> HRManagerList()
        {
            try
            {
                var hrManagerList = await _hrManagerService.GetHRManagerList();

                if (hrManagerList == null || !hrManagerList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = hrManagerList;
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

        [HttpGet("gethrbyid")]
        public async Task<IActionResult> GetAdvisorById(int Id)
        {
            try
            {
                var hrManager = await _hrManagerService.GetHRManagerById(Id);
                if (hrManager == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = hrManager;
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

        [HttpPost("addhr")]
        public async Task<IActionResult> AddHRManager([FromForm] HRManager hrManager, IFormFile imageFile, IFormFile docFile)
        {
            try
            {
                var result = await _hrManagerService.AddHRManager(hrManager, imageFile, docFile);

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

        [HttpPut("updatehr")]
        public async Task<IActionResult> UpdateHRManager(HRManagerDTO hrManagerDTO)
        {
            try
            {
                var result = await _hrManagerService.UpdateHRManager(hrManagerDTO);

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

        [HttpPut("updatehrprofile")]
        public async Task<IActionResult> UpdateHRManagerProfile([FromForm] HRManagerProfileDTO profileDTO)
        {
            try
            {
                var result = await _hrManagerService.UpdateHRManagerProfile(profileDTO);

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
        [HttpPut("updatehrdocument")]
        public async Task<IActionResult> UpdateHRManagerDocument([FromForm] HRManagerDocumentDTO documentDTO)
        {
            try
            {
                var result = await _hrManagerService.UpdateHRManagerDocument(documentDTO);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = "updated";
                return Ok(_response.Result);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.Unauthorized;
                _response.Result = "file corrupted";

                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("deletehr")]
        public async Task<IActionResult> DeleteHRManager(int id)
        {
            try
            {
                await _hrManagerService.DeleteHRManager(id);
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
