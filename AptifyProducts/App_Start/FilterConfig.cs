using AptifyWebApi.Attributes;
using System.Web;
using System.Web.Mvc;

namespace AptifyWebApi {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}