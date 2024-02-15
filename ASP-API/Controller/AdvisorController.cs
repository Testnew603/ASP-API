using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Services.Shared;
using ASP_API.Services.Staff;
using ASP_API.Services.Student;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;

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

        [HttpGet("AdvisorList")]
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

        [HttpGet("GetAdvisorById")]
        public async Task<IActionResult> GetAdvisorById(int Id)
        {
            try
            {
                var advisor = await _advisorService.GetAdvisorById(Id);

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                var imagesPath = "/Profiles/advisor";
                var imageUrl = baseUrl + imagesPath + "/" + advisor.Profile;
                
                advisor.Profile = imageUrl;      

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

        [HttpPost("AddAdvisor")]
        public async Task<IActionResult> AddAdvisor([FromForm] Advisor advisor, IFormFile imageFile, IFormFile docFile)
        {
            try
            {

                var result = await _advisorService.AddAdvisor(advisor, imageFile, docFile);

                _response.IsSuccess = true;
                _response.Result = "Your account has been sent for approval. Once it is approved you will get an email.";
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

        [HttpPut("UpdateAdvisor")]
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

        [HttpPut("UpdateAdvisorStatus")]
        public async Task<IActionResult> UpdateAdvisorStatus(AdvisorStatusUpdateDTO statusUpdateDTO)
        {
            try
            {
                var result = await _advisorService.UpdateAdvisorStatus(statusUpdateDTO);

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

        [HttpPut("UpdateAdvisorProfile")]
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
        [HttpPut("UpdateAdvisorDocument")]
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

        [HttpDelete("DeleteAdvisor")]
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





        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var files = Request.Form.Files;
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Profiles", "advisor");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok("All the files are successfully uploaded.");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
