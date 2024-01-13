using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewUpdateController : ControllerBase
    {
        private readonly IReviewUpdateService _reviewUpdateService;
        private readonly ResponseMessages _response;

        public ReviewUpdateController(IReviewUpdateService reviewUpdateService, ResponseMessages response)
        {
            _reviewUpdateService = reviewUpdateService;
            _response = response;
        }

        [HttpGet("reviewList")]
        public async Task<IActionResult> ReviewList()
        {
            try
            {
                var batchList = await _reviewUpdateService.GetReviewDetailsList();

                if (batchList == null || !batchList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = batchList;
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

        [HttpGet("getReviewById")]
        public async Task<IActionResult> GetReviewById(int Id)
        {
            try
            {
                var reviewdetails = await _reviewUpdateService.GetReviewDetailsById(Id);
                if (reviewdetails == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = reviewdetails;
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

        [HttpPost("addReview")]
        public async Task<IActionResult> AddReviewDetails(ReviewUpdates reviewUpdates)
        {
            try
            {
                var result = await _reviewUpdateService.AddReviewDetails(reviewUpdates);

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

        [HttpPut("updateReviewDetails")]
        public async Task<IActionResult> UpdateReviewDetails(ReviewUpdates reviewUpdates)
        {
            try
            {
                var result = await _reviewUpdateService.UpdateReviewDetails(reviewUpdates);

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

        [HttpDelete("deleteReviewDetails")]
        public async Task<IActionResult> DeleteReviewDetails(int id)
        {
            try
            {
                await _reviewUpdateService.DeleteReviewDetails(id);
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

        //Review summary crud 

        [HttpGet("reviewSummaryList")]
        public async Task<IActionResult> ReviewSummaryList()
        {
            try
            {
                var reviewSummary = await _reviewUpdateService.GetReviewSummaryList();

                if (reviewSummary == null || !reviewSummary.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = reviewSummary;
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

        [HttpGet("getReviewSummaryById")]
        public async Task<IActionResult> GetReviewSummaryById(int Id)
        {
            try
            {
                var summaryDetails = await _reviewUpdateService.GetReviewSummaryById(Id);
                if (summaryDetails == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = summaryDetails;
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

        [HttpPost("addReviewSummary")]
        public async Task<IActionResult> AddReviewSummary(ReviewSummary reviewSummary)
        {
            try
            {
                var result = await _reviewUpdateService.AddReviewSummary(reviewSummary);

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

        [HttpPut("updateReviewSummary")]
        public async Task<IActionResult> UpdateReviewDetails(ReviewSummary reviewSummary)
        {
            try
            {
                var result = await _reviewUpdateService.UpdateReviewSummary(reviewSummary);

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

        [HttpDelete("deleteReviewSummary")]
        public async Task<IActionResult> DeleteReviewSummary(int id)
        {
            try
            {
                await _reviewUpdateService.DeleteReviewSummary(id);
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
