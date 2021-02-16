using System.Collections.Generic;

namespace Web.HttpAggregator.Config
{
    public class ServiceConfig
    {
        public class AuthOperations
        {
            public static string Validate(string token) => $"Auth/Validate?token={token}";

            public static string CheckRight(string service) => $"Right/CheckRight?serviceName={service}";
        }

        public class RestaurantOperations
        {
            public static string GetRestaurants() => "Dropdown/GetRestaurants";
        }

        public string Auth { get; set; }

        public string Restaurant { get; set; }

        public string Order { get; set; }
    }
}
