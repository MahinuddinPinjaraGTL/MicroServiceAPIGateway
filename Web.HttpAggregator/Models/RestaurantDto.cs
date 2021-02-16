using RestaurantEntities.Entities;

namespace Web.HttpAggregator.Models
{
    public class RestaurantDto
    {
        public RestaurantDto()
        {
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
        public int StaffMembers { get; set; }
    }
}
