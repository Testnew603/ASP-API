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
    public class GeneralManagerController : ControllerBase
    {
        private readonly IGeneralManagerService _managerService;
        private ResponseMessages _response;

        public GeneralManagerController(IGeneralManagerService managerService, ResponseMessages response)
        {
            _managerService = managerService;
            _response = response;
        }

        [HttpGet("managerlist")]
        public async Task<IActionResult> ManagerList()
        {
            try
            {
                var managerList = await _managerService.GetGeneralManagerList();

                if (managerList == null || !managerList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = managerList;
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

        [HttpGet("getmanagerbyid")]
        public async Task<IActionResult> GetManagerById(int Id)
        {
            try
            {
                var manager = await _managerService.GetGeneralManagerById(Id);
                if (manager == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = manager;
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

        [HttpPost("addmanager")]
        public async Task<IActionResult> AddManager([FromForm] GeneralManager manager, IFormFile imageFile, IFormFile docFile)
        {
            try
            {
                var result = await _managerService.AddGeneralManager(manager, imageFile, docFile);

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

        [HttpPut("updatemanager")]
        public async Task<IActionResult> UpdateManager(GeneralManagerDTO managerDTO)
        {
            try
            {
                var result = await _managerService.UpdateGeneralManager(managerDTO);

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

        [HttpPut("updatemanagerprofile")]
        public async Task<IActionResult> UpdateManagerProfile([FromForm] GeneralManagerProfileDTO managerProfileDTO)
        {
            try
            {
                var result = await _managerService.UpdateGeneralManagerProfile(managerProfileDTO);

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
        [HttpPut("updatemanagerdocument")]
        public async Task<IActionResult> UpdateManagerDocument([FromForm] GeneralManagerDocumentDTO managerDocumentDTO)
        {
            try
            {
                var result = await _managerService.UpdateGeneralManagerDocument(managerDocumentDTO);
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

        [HttpDelete("deletemanager")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            try
            {
                await _managerService.DeleteGeneralManager(id);
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
