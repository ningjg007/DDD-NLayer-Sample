using System;
using System.Collections.Generic;
using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IRoleService
    {
        RoleDTO Add(RoleDTO roleDTO);

        void Update(RoleDTO roleDTO);

        void Remove(Guid id);

        List<RoleDTO> FindAll();

        RoleDTO FindBy(Guid id);

        IPagedList<RoleDTO> FindBy(Guid roleGroupId, string name, int pageNumber, int pageSize);

        void UpdateRolePermission(Guid id, List<Guid> permissions);

        List<PermissionDTO> GetRolePermission(Guid id);
    }
}
