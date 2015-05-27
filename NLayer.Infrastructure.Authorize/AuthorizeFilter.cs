using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NLayer.Infrastructure.Authorize
{
    public class AuthorizeFilter : AuthorizeAttribute
    {
        public IAuthorizeManager AuthorizeManager { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                ValidatePermission(filterContext);
            }
        }

        private void ValidatePermission(AuthorizationContext filterContext)
        {
            var identity = filterContext.HttpContext.User.Identity as FormsIdentity;
            if (identity == null)
            {
                filterContext.Result = new HttpUnauthorizedResult("Unauthorized");
                return;
            }

            try
            {
                var token = AuthorizeManager.GetCurrentTokenFromCookies();
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new AuthorizeTokenNotFoundException();
                }

                var permissionAttributes =
                    filterContext.ActionDescriptor.GetCustomAttributes(typeof (PermissionAttribute), false)
                        .Cast<PermissionAttribute>();

                var attributes = permissionAttributes as IList<PermissionAttribute> ?? permissionAttributes.ToList();
                if (attributes.Any(x => !AuthorizeManager.ValidatePermission(x.Code)))
                {
                    throw new AuthorizeNoPermissionException();
                }
            }
            catch (AuthorizeTokenInvalidException)
            {
                filterContext.Result = new HttpUnauthorizedResult("Invalid Token");
            }
            catch (AuthorizeTokenNotFoundException)
            {
                filterContext.Result = new HttpUnauthorizedResult("Token Not Found");
            }
            catch (AuthorizeNoPermissionException)
            {
                filterContext.Result = new HttpUnauthorizedResult("No Permission");
            }
            catch (Exception ex)
            {
                filterContext.Result = new HttpUnauthorizedResult(ex.Message);
            }
        }
    }
}
