using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLayer.Infrastructure.Authorize;

namespace NLayer.Presentation.WebHost.Controllers
{
    public class HomeController : BaseAuthorizeController
    {
        protected override void CheckLogin()
        {
            //base.CheckoutLogin();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}