using ASP_API.DTO;
using ASP_API.Model.Public;
using ASP_API.Model.Staff;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly ResponseMessages _response;
        private readonly IBatchServices _batchServices;

        public BatchController(IBatchServices batchServices, ResponseMessages response)
        {
            _batchServices = batchServices;
            _response = response;
        }

        [HttpGet("batchlist")]
        public async Task<IActionResult> BatchList()
        {
            try
            {
                var batchList = await _batchServices.GetBatchList();

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

        [HttpGet("getbatchbyid")]
        public async Task<IActionResult> GetBatchById(int Id)
        {
            try
            {
                var batch = await _batchServices.GetBatchById(Id);
                if (batch == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = batch;
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

        [HttpPost("addbatch")]
        public async Task<IActionResult> AddBatch(Batch batch)
        {
            try
            {
                var result = await _batchServices.AddBatch(batch);

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

        [HttpPut("updatebatch")]
        public async Task<IActionResult> UpdateBatch(Batch batch)
        {
            try
            {
                var result = await _batchServices.UpdateBatch(batch);

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

        [HttpDelete("deletebatch")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            try
            {
                await _batchServices.DeleteBatch(id);
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

        //batch allocation to advisor crud

        [HttpGet("batchListAllocatedToAdvisor")]
        public async Task<IActionResult> BatchListAllocatedToAdvisor()
        {
            try
            {
                var result = await _batchServices.GetAllocBatchToAdvisorList();

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

        [HttpGet("getBatchAllocToAdvisorById")]
        public async Task<IActionResult> GetBatchAllocToAdvisorById(int Id)
        {
            try
            {
                var result = await _batchServices.GetAllocBatchToAdvisorById(Id);
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

        [HttpPost("BatchAllocToAdvisor")]
        public async Task<IActionResult> BatchAllocToAdvisor(AllocBatchToAdvisor allocBatchToAdvisor)
        {
            try
            {
                var result = await _batchServices.AllocBatchToAdvisor(allocBatchToAdvisor);

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

        [HttpPut("updateBatchAllocToAdvisor")]
        public async Task<IActionResult> UpdateBatchAllocToAdvisor(AllocBatchToAdvisor allocBatchToAdvisor)
        {
            try
            {
                var result = await _batchServices.UpdateAllocBatchToAdvisor(allocBatchToAdvisor);

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

        [HttpDelete("deleteBatchAllocToAdvisor")]
        public async Task<IActionResult> DeleteBatchAllocToAdvisor(int id)
        {
            try
            {
                await _batchServices.DeleteAllocBatchToAdvisor(id);
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


        //Batch & Branch allocation to student crud
        [HttpGet("batchListAllocatedToStudent")]
        public async Task<IActionResult> BatchListAllocatedToStudent()
        {
            try
            {
                var result = await _batchServices.GetAllocBatchToStudList();

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

        [HttpGet("getBatchAllocToStudById")]
        public async Task<IActionResult> GetBatchAllocToStudentById(int Id)
        {
            try
            {
                var result = await _batchServices.GetAllocBatchToStudById(Id);
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

        [HttpPost("BatchAllocToStudent")]
        public async Task<IActionResult> BatchAllocToStudent(AllocBatchBranchToStudent allocBatchBranchToStudent)
        {
            try
            {
                var result = await _batchServices.AllocBatchToStud(allocBatchBranchToStudent);

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

        [HttpPut("updateBatchAllocToStudent")]
        public async Task<IActionResult> UpdateBatchAllocToStudent(AllocBatchBranchToStudent allocBatchBranchToStudent)
        {
            try
            {
                var result = await _batchServices.UpdateAllocBatchToStud(allocBatchBranchToStudent);

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

        [HttpDelete("deleteBatchAllocToStudent")]
        public async Task<IActionResult> DeleteBatchAllocToStudent(int id)
        {
            try
            {
                await _batchServices.DeleteAllocBatchToStud(id);
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

        //Batch & Branch allocation to student crud
        [HttpGet("batchAllocatedToTrainerList")]
        public async Task<IActionResult> GetAllocBatchToTrainerList()
        {
            try
            {
                var result = await _batchServices.GetAllocBatchToTrainerList();

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

        [HttpGet("getBatchAllocToTrainerById")]
        public async Task<IActionResult> GetAllocBatchToTrainerById(int Id)
        {
            try
            {
                var result = await _batchServices.GetAllocBatchToTrainerById    (Id);
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

        [HttpPost("BatchAllocToTrainer")]
        public async Task<IActionResult> BatchAllocToTrainer(AllocBatchToTrainer allocBatchToTrainer)
        {
            try
            {
                var result = await _batchServices.AllocBatchToTrainer(allocBatchToTrainer);

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

        [HttpPut("updateBatchAllocToTrainer")]
        public async Task<IActionResult> UpdateBatchAllocToTrainer(AllocBatchToTrainer allocBatchToTrainer)
        {
            try
            {
                var result = await _batchServices.UpdateAllocBatchToTrainer(allocBatchToTrainer);

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

        [HttpDelete("deleteBatchAllocToTrainer")]
        public async Task<IActionResult> DeleteBatchAllocToTrainer(int id)
        {
            try
            {
                await _batchServices.DeleteAllocBatchToTrainer(id);
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
