using System.Threading.Tasks;

namespace Web.HttpAggregator.Repositories
{
    public interface IIdentityRepository
    {
        Task<bool> Validate(string token);
    }
}
