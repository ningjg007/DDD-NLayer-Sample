using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using NLayer.Application.Managers;
using NLayer.Infrastructure.Authorize;
using NLayer.Infrastructure.Authorize.AuthObject;
using NLayer.Presentation.WebHost.App_Start;
using NLayer.Presentation.WebHost.Helper;

namespace NLayer.Presentation.WebHost.Controllers
{
    public class BaseAuthorizeController : BaseController
    {
        private NLayerServiceResolver _serviceResolver;

        public NLayerServiceResolver ServiceResolver
        {
            get { return _serviceResolver ?? (_serviceResolver = new NLayerServiceResolver()); }
        }

        //public IServiceResolver ServiceResolver { get; set; }

        private IAuthorizeManager AuthorizeManager
        {
            get
            {
                return ServiceResolver.Resolve<IAuthorizeManager>();
            }
        }

        private UserForAuthorize _CurrentUser;

        public BaseAuthorizeController()
        {
            ViewBag.CurrentUser = GetCurrentUser();
        }

        protected UserForAuthorize GetCurrentUser()
        {
            try
            {
                _CurrentUser = AuthorizeManager.GetCurrentUserInfo();
            }
            catch (AuthorizeTokenNotFoundException)
            {
            }
            return _CurrentUser;
        }

        protected virtual void CheckLogin()
        {
            if (GetCurrentUser() == null)
            {
                AuthorizeManager.RedirectToLoginPage();
            }
            
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            CheckLogin();
        }
    }
}