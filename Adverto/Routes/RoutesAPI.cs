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
        public static class CategoryRoutes
        {
            public const string getCategory = baseUrl + "/Category/{categoryId}";
            public const string getCategories = baseUrl + "/Categories";
            public const string removeCategory = baseUrl + "/Category/{categoryId}";
            public const string addCategory = baseUrl + "/createCategory";
            public const string updateCategory = baseUrl + "/Category/{categoryId}";
        }
        public static class SubCategory
        {
            public const string getSubCategory = baseUrl + "/SubCategory/{subcategoryId}";
            public const string getSubCategories = baseUrl + "/SubCategories";
            public const string removeSubCategory = baseUrl + "/SubCategory/{subcategoryId}";
            public const string updateSubCategory = baseUrl + "/SubCategory/{subcategoryId}";
            public const string createSubCategory = baseUrl + "/createSubCategory";
        }
    }
}
