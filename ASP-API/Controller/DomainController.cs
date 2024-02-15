using Abp.Domain.Services;
using ASP_API.Model.Public;
using ASP_API.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace ASP_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainController : ControllerBase
    {
        private readonly IDomainServices _domainServices;
        private ResponseMessages _response;

        public DomainController(IDomainServices domainServices, ResponseMessages response)
        {
            _domainServices = domainServices;
            _response = response;         
        }

        [HttpGet("DomainList")]
        public async Task<IActionResult> DomainList()
        {
            try
            {
                var domainList = await _domainServices.GetDomainList();

                //var ipAddress = HttpContext.Connection.RemoteIpAddress;
                //Console.WriteLine(ipAddress);
                //var ipadd = _domainServices.GetLocalIpAddress();

                if (domainList == null || !domainList.Any())
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = domainList;
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

        [HttpGet("GetDomainById")]
        public async Task<IActionResult> GetDomainById(int Id)
        {
            try
            {
                var domain = await _domainServices.GetDomainById(Id);
                if (domain == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent);
                }

                _response.IsSuccess = true;
                _response.Result = domain;
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

        [HttpPost("adddomain")]
        public async Task<IActionResult> AddDomain(Domain domain)
        {
            try
            {
                var result = await _domainServices.AddDomain(domain);

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

        [HttpPut("updatedomain")]
        public async Task<IActionResult> UpdateDomain(Domain domain)
        {
            try
            {
                var result = await _domainServices.UpdateDomain(domain);

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

        [HttpDelete("deletedomain")]
        public async Task<IActionResult> DeleteDomain(int id)
        {
            try
            {
                await _domainServices.DeleteDomain(id);
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
