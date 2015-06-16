using System;
using System.Collections.Generic;
using NLayer.Application.CommonModule.DTOs;
using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IUserService
    {
        UserDTO Add(UserDTO userDTO);

        void Update(UserDTO userDTO);

        void Remove(Guid id);

        UserDTO FindBy(Guid id);

        IPagedList<UserDTO> FindBy(string name, int pageNumber, int pageSize);

        void UpdateUserPermission(Guid id, List<Guid> permissions);

        List<PermissionDTO> GetUserPermission(Guid id);

        List<IdNameDTO> GetAllUsersIdName();
    }
}
