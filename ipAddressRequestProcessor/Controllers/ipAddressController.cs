using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ipAddressRequestProcessor.Services;
using ipAddressRequestProcessor.Models;

namespace ipAddressRequestProcessor.Controllers
{
    public class ipAddressController : baseController
    {
        IIpAddressLookUpService _ipLookUpService;

        public ipAddressController(IIpAddressLookUpService ipLookUpService) : base()
        {
            _ipLookUpService = ipLookUpService;
        }

        [HttpGet(Name = "ipAddress")]
        public async Task<ActionResult<ipAddress>> Get(string ipAddress)
        {
            try
            {
                var _ip = await _ipLookUpService.check(ipAddress);
                return Ok(_ip);
            }
            catch (Exception ex)
            {
                // more appropriate handle method would be suitable but we are not going to worry about it at this time
                return Problem(ex.Message);
            }
        }
    }
}
