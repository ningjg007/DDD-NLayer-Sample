using System;
using System.Collections.Generic;
using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IAuthService
    {
        UserDTO Login(string loginName, string password, bool updateLoginToken);

        bool ValidatePassword(Guid id, string password);

        void ChangePassword(Guid id, string password);

        bool ValidateLoginToken(Guid id, string token);

        UserDTO FindByLoginToken(Guid id, string token);

        List<PermissionForAuthDTO> GetRolePermissions(Guid id);

        List<PermissionForAuthDTO> GetUserPermissions(Guid id);
    }
}
