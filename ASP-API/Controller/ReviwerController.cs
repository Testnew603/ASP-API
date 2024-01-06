using ASP_API.DTO;
using ASP_API.Migrations;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Services.Staff;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviwerController : ControllerBase
    {
        private readonly IReviwerService _reviwerService;
        private ResponseMessages _response;

        public ReviwerController(IReviwerService reviwerService, ResponseMessages response)
        {
            _response = response;
            _reviwerService = reviwerService;
        }

        [HttpGet("reviewerlist")]
        public async Task<IActionResult> ReviewerList()
        {
            try
            {
                var reviewerList = await _reviwerService.GetReviewerList();

                if (reviewerList == null || !reviewerList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = reviewerList;
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

        [HttpGet("getreviewerbyid")]
        public async Task<IActionResult> GetReviewerById(int Id)
        {
            try
            {
                var reviwer = await _reviwerService.GetReviewerById(Id);
                if (reviwer == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = reviwer;
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

        [HttpPost("addreviwer")]
        public async Task<IActionResult> AddReviwer([FromForm] Reviewer reviewer, IFormFile imageFile)
        {
            try
            {
                var result = await _reviwerService.AddReviewer(reviewer, imageFile);

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

        [HttpPut("updatereviwer")]
        public async Task<IActionResult> UpdateReviewer(ReviewerDTO reviewerDTO)
        {
            try
            {
                var result = await _reviwerService.UpdateReviewer(reviewerDTO);

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

        [HttpPut("updatereviewerprofile")]
        public async Task<IActionResult> UpdateReviewerProfile([FromForm] ReviwerProfileDTO profileDTO)
        {
            try
            {
                var result = await _reviwerService.UpdateReviewerProfile(profileDTO);

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

        [HttpDelete("deletereviewer")]
        public async Task<IActionResult> DeleteReviewer(int id)
        {
            try
            {
                await _reviwerService.DeleteReviewer(id);
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
