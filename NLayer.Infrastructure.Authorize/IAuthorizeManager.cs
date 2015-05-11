using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Infrastructure.Authorize.AuthObject;

namespace NLayer.Infrastructure.Authorize
{
    public interface IAuthorizeManager
    {
        string GetCurrentTokenFromCookies();

        UserForAuthorize GetCurrentUserInfo();

        void SignIn(string loginName, string password, bool rememberMe = false);

        void SignOut();

        bool ValidatePermission(string permissionCode, bool throwExceptionIfNotPass = true);
    }
}
