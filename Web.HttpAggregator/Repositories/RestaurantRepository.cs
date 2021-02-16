using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Util;

namespace Web.HttpAggregator.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ILogger<RestaurantRepository> _logger;
        private readonly ServiceConfig _urls;
        public RestaurantRepository(ILogger<RestaurantRepository> logger, IOptions<ServiceConfig> config)
        {
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<List<RestaurantDto>> GetRestaurantDropdown()
        {
            var url = _urls.Restaurant + ServiceConfig.RestaurantOperations.GetRestaurants();
            _logger.LogInformation("Write your business logic.");
            var restaurants = await HttpCall.GetRequest<List<RestaurantDto>>(url);

            // Driver service
            var urlRight = _urls.Auth + ServiceConfig.AuthOperations.CheckRight("Driver");
            var check = await HttpCall.GetRequest<bool>(urlRight);

            if (check)
            {
                // Call driver api
            }
            _logger.LogInformation("Restaurant dropdown detail as response");
            return restaurants;
        }
    }
}
