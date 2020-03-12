using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Routes
{
    public  static class RoutesAPI
    {
        public const string baseUrl = "/api";

        public static class AdvertRoutes
        {
            public const string GetAdvert = baseUrl + "/Advert/{advertId}";
            public const string GetAdverts = baseUrl + "/Adverts";
            public const string RemoveAdvert = baseUrl + "/Advert/{advertId}";
            public const string addAdvert = baseUrl + "/create";
            public const string updateAdvert = baseUrl + "/Advert/{advertId}";

        }
    }
}
