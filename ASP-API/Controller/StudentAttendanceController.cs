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
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IStudentAttendanceService _studentAttendanceService;
        private readonly ResponseMessages _response;

        public StudentAttendanceController(IStudentAttendanceService studentAttendanceService,
            ResponseMessages response)
        {
            _studentAttendanceService = studentAttendanceService;
            _response = response;
        }

        [HttpGet("GetStudentAttendanceList")]
        public async Task<IActionResult> GetStudentAttendanceList()
        {
            try
            {
                var attendanceList = await _studentAttendanceService.GetStudentAttendanceList();

                if (attendanceList == null || !attendanceList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = attendanceList;
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

        [HttpGet("GetStudentAttendanceById")]
        public async Task<IActionResult> GetStudentAttendance(int Id)
        {
            try
            {
                var attendance = await _studentAttendanceService.GetStudentAttendance(Id);
                if (attendance == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = attendance;
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

        [HttpPost("AddStudentAttendanceEntry")]
        public async Task<IActionResult> AddStudentAttendanceEntry(StudAttendanceEntryDTO attendanceEntryDTO)
        {
            try
            {
                var result = await _studentAttendanceService.AddStudentAttendanceEntry(attendanceEntryDTO);

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

        [HttpPut("AddStudentAttendanceExit")]
        public async Task<IActionResult> AddStudentAttendanceExit(StudAttendanceExitDTO attendanceExitDTO)
        {
            try
            {
                var result = await _studentAttendanceService.AddStudentAttendanceExit(attendanceExitDTO);

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

        [HttpPut("UpdateStudentAttendance")]
        public async Task<IActionResult> UpdateStudentAttendance(StudentAttendance studentAttendance)
        {
            try
            {
                var result = await _studentAttendanceService.UpdateStudentAttendance(studentAttendance);

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

        [HttpDelete("DeleteStudentAttendance")]
        public async Task<IActionResult> DeleteStudentAttendance(int id)
        {
            try
            {
                await _studentAttendanceService.DeleteStudentAttendance(id);
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
