using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.Infrastructure.Authorize;
using NLayer.Presentation.WebHost.Controllers;

namespace NLayer.Presentation.WebHost.Areas.Samples.Controllers
{
    public class BootstrapController : BaseAuthorizeController
    {
        protected override void CheckLogin()
        {
            //base.CheckoutLogin();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buttons()
        {
            return View();
        }

        public ActionResult Elements()
        {
            return View();
        }

        public ActionResult Tables()
        {
            return View();
        }

        public ActionResult Forms()
        {
            return View();
        }

        public ActionResult Layouts()
        {
            return View();
        }
    }
}