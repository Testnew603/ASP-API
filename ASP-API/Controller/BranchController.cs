using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchServices _branchServices;
        private ResponseMessages _response;

        public BranchController(IBranchServices branchServices, ResponseMessages response)
        {
            _branchServices = branchServices;
            _response = response;
        }

        [HttpGet("branchlist")]
        public async Task<IActionResult> BranchList()
        {
            try
            {
                var batchList = await _branchServices.GetBranchList();

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

        [HttpGet("getbranchbyid")]
        public async Task<IActionResult> GetBranchById(int Id)
        {
            try
            {
                var branch = await _branchServices.GetBranchById(Id);
                if (branch == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = branch;
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

        [HttpPost("addbranch")]
        public async Task<IActionResult> AddBranch(Branch branch)
        {
            try
            {
                var result = await _branchServices.AddBranch(branch);

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

        [HttpPut("updatebranch")]
        public async Task<IActionResult> UpdateBranch(Branch branch)
        {
            try
            {
                var result = await _branchServices.UpdateBranch(branch);

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

        [HttpDelete("deletebranch")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                await _branchServices.DeleteBranch(id);
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
