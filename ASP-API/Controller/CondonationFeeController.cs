using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondonationFeeController : ControllerBase
    {
        private readonly ICondonationFeeService _condonationFee;
        private readonly ResponseMessages _response;

        public CondonationFeeController(ICondonationFeeService condonationFee, ResponseMessages response)
        {
            _condonationFee = condonationFee;
            _response = response;
        }

        [HttpGet("condonationfeelist")]
        public async Task<IActionResult> CondonationFeeList()
        {
            try
            {
                var condonationFee = await _condonationFee.GetCondonationFeeList();

                if (condonationFee == null || !condonationFee.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = condonationFee;
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

        [HttpGet("getcondonationfeebyid")]
        public async Task<IActionResult> GetCondonationFeeById(int Id)
        {
            try
            {
                var condonationFee = await _condonationFee.GetCondonationFeeById(Id);
                if (condonationFee == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = condonationFee;
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

        [HttpPost("addcondonationfee")]
        public async Task<IActionResult> AddCondonationFee(CondonationFees condonationFees)
        {
            try
            {
                var result = await _condonationFee.AddCondonationFee(condonationFees);

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

        [HttpPut("updatecondonationfee")]
        public async Task<IActionResult> UpdateCondonationFee(CondonationFees condonationFees)
        {
            try
            {
                var result = await _condonationFee.UpdateCondonationFee(condonationFees);

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

        [HttpDelete("deletecondonationfee")]
        public async Task<IActionResult> DeleteCondonationFee(int id)
        {
            try
            {
                await _condonationFee.DeleteCondonationFee(id);
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
