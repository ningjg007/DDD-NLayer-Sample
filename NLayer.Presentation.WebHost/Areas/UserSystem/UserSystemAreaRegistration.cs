using System.Web.Mvc;

namespace NLayer.Presentation.WebHost.Areas.UserSystem
{
    public class UserSystemAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserSystem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserSystem_default",
                "UserSystem/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}