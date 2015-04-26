using System.Web;
using System.Web.Mvc;

namespace NLayer.Presentation.WebHost
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomAjaxExceptionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
