using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NLayer.Application.Exceptions;
using NLayer.Infrastructure.Authorize;
using NLayer.Presentation.WebHost.Models;

namespace NLayer.Presentation.WebHost.Controllers
{
    public class AccountController : BaseController
    {
        IAuthorizeManager AuthorizeManager;

        public AccountController(IAuthorizeManager authorizeManager)
        {
            AuthorizeManager = authorizeManager;
        }

        public ActionResult Login()
        {
            var returnUrl = Request["ReturnUrl"] ?? "/";
            if (returnUrl.IndexOf("Logout", StringComparison.OrdinalIgnoreCase) > -1)
            {
                returnUrl = "/";
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult Forbidden()
        {
            return View();
        }

        public ActionResult Logout()
        {
            AuthorizeManager.SignOut();
            AuthorizeManager.RedirectToLoginPage();
            return null;
        }

        [HttpPost]
        public ActionResult SignIn(string returnUrl, string loginName, string password, bool? rememberMe)
        {
            var response = new AjaxResponse();
            try
            {
                AuthorizeManager.SignIn(loginName, password, rememberMe.HasValue && rememberMe.Value);
                response.Succeeded = true;
                response.RedirectUrl = returnUrl;
            }
            catch (Exception ex)
            {
                if (!(ex is DefinedException))
                {
                    Log.Error(ex.GetIndentedExceptionLog());
                }
                response.Succeeded = false;
                response.ErrorMessage = ex.Message;
                response.ShowMessage = true;
            }
            return  Json(response);
        }
    }
}