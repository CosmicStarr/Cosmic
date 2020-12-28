using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmicWeb.Models
{
    public class StaticDetails
    {
        public static string ApiBaseUrl = "https://localhost:44395/";
        public static string CosmicSpotApiPath = ApiBaseUrl + "api/v1/cosmicspot/";
        public static string CosmicDirectionApiPath = ApiBaseUrl + "api/v1/directions/";
    }
}
