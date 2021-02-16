using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Web.HttpAggregator.Attributes;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Repositories;

namespace Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [TypeFilter(typeof(CustomAuthorization))]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantRepository _repository;

        public RestaurantController(IRestaurantRepository repository, ILogger<RestaurantController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        ///  Get restaurants
        /// </summary>
        /// <returns></returns>
        [Route("GetRestaurants")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<RestaurantDto>>> GetRestaurants()
        {
            var response = await _repository.GetRestaurantDropdown();
            if (response == null)
            {
                return BadRequest("No detail available");
            }
            return response;
        }
    }
}
