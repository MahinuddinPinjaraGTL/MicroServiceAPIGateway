using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Config;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Util;

namespace Web.HttpAggregator.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ILogger<IdentityRepository> _logger;
        private readonly ServiceConfig _urls;
        public IdentityRepository(ILogger<IdentityRepository> logger, IOptions<ServiceConfig> config)
        {
            _logger = logger;
            _urls = config.Value;
        }

        public async Task<bool> Validate(string token)
        {
            var url = _urls.Auth + ServiceConfig.AuthOperations.Validate(token: token);
            _logger.LogInformation("Write your business logic.");
            var isValid = await HttpCall.GetRequest<bool>(url);
            _logger.LogInformation("Validate token detail as response");
            return isValid;
        }
    }
}
