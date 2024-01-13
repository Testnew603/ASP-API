using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingCommunicationController : ControllerBase
    {
        private readonly ITrainingAndCommunicationService _trainingAndCommunicationService;
        private readonly ResponseMessages _response;

        public TrainingCommunicationController(
            ITrainingAndCommunicationService trainingAndCommunicationService,
            ResponseMessages response)
        {
            _trainingAndCommunicationService = trainingAndCommunicationService;
            _response = response;
        }
        
        [HttpGet("getTrainingScheduleList")]
        public async Task<IActionResult> GetTrainingScheduleList()
        {
            try
            {
                var result = await _trainingAndCommunicationService.GetTrainingScheduleList();

                if (result == null || !result.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = result;
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

        [HttpGet("getTrainingScheduleById")]
        public async Task<IActionResult> GetTrainingScheduleById(int Id)
        {
            try
            {
                var result = await _trainingAndCommunicationService.GetTrainingScheduleById(Id);
                if (result == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = result;
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

        [HttpPost("addTrainingSchedule")]
        public async Task<IActionResult> AddTrainingSchedule(TrainingAndCommunication trainingAndCommunication)
        {
            try
            {
                var result = await _trainingAndCommunicationService.AddTrainingSchedule(trainingAndCommunication);

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

        [HttpPut("updateTrainingSchedule")]
        public async Task<IActionResult> UpdateTrainingSchedule(TrainingAndCommunication trainingAndCommunication)
        {
            try
            {
                var result = await _trainingAndCommunicationService.UpdateTrainingSchedule(trainingAndCommunication);

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

        [HttpDelete("deleteTrainingSchedule")]
        public async Task<IActionResult> DeleteBatchAllocToStudent(int id)
        {
            try
            {
                await _trainingAndCommunicationService.DeleteTrainingSchedule(id);
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
