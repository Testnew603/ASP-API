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
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly ResponseMessages _response;

        public TrainerController(ITrainerService trainerService, ResponseMessages response)
        {
            _trainerService = trainerService;
            _response = response;
        }

        [HttpGet("trainerlist")]
        public async Task<IActionResult> TrainerList()
        {
            try
            {
                var trainerList = await _trainerService.GetTrainerList();

                if (trainerList == null || !trainerList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = trainerList;
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

        [HttpGet("gettrainerbyid")]
        public async Task<IActionResult> GetTrainerById(int Id)
        {
            try
            {
                var trainer = await _trainerService.GetTrainerById(Id);
                if (trainer == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = trainer;
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

        [HttpPost("addtrainer")]
        public async Task<IActionResult> AddTrainer([FromForm] Trainer trainer, IFormFile imageFile)
        {
            try
            {
                var result = await _trainerService.AddTrainer(trainer, imageFile);

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

        [HttpPut("updatetrainer")]
        public async Task<IActionResult> UpdateTrainer(TrainerDTO trainerDTO)
        {
            try
            {
                var result = await _trainerService.UpdateTrainer(trainerDTO);

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

        [HttpPut("updatetrainerprofile")]
        public async Task<IActionResult> UpdateTrainerProfile([FromForm] TrainerProfileDTO profileDTO)
        {
            try
            {
                var result = await _trainerService.UpdateTrainerProfile(profileDTO);

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

        [HttpDelete("deletetrainer")]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            try
            {
                await _trainerService.DeleteTrainer(id);
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
