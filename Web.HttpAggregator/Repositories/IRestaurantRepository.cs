using System.Collections.Generic;
using System.Threading.Tasks;
using Web.HttpAggregator.Models;

namespace Web.HttpAggregator.Repositories
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantDto>> GetRestaurantDropdown();
    }
}
