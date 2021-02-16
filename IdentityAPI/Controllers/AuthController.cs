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
    public class AuthController : ControllerBase
    {

        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [Route("Login")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoginDto>> Login(string username, string password)
        {
            if (username != "admin" && password != "admin")
            {
                return BadRequest("Invalid credential");
            }

            return new LoginDto
            {
                FirstName = "Admin",
                LastName = "Admin",
                Token = "NewToken"
            };
        }

        [Route("Validate")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> Validate(string token)
        {
            if (string.IsNullOrEmpty(token) || token != "mytoken")
            {
                return BadRequest("Invalid token");
            }
            return true;
        }
    }
}
