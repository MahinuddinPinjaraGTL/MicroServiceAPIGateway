using IdentityAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RightController : ControllerBase
    {

        private readonly ILogger<RightController> _logger;

        public RightController(ILogger<RightController> logger)
        {
            _logger = logger;
        }

        [Route("CheckRight")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CheckRight(string serviceName)
        {
            // Get data from database using userid (Claim)
            var services = new List<UserService>
            {
                new UserService
                {
                    Name = "Customer"
                },
                 new UserService
                {
                    Name = "Driver"
                },
                  new UserService
                {
                    Name = "Restuarant"
                }
            };

            var service = services.Find(service => service.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase));

            if (service == null)
            {
                return BadRequest($"Don't have access {serviceName}");
            }

            return true;
        }
    }
}
