using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using NLayer.Presentation.WebHost.App_Start;

namespace NLayer.Presentation.WebHost
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            Log.Info("Application starting...");

            AutofacConfig.Config();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = true;
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Info("Application started.");
        }
        protected void Application_End(object sender, EventArgs e)
        {
            Log.Info("Application stopped.");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            var log = LogManager.GetLogger(ex.GetType());
            log.Error(ex.GetIndentedExceptionLog());
        }
    }
}
