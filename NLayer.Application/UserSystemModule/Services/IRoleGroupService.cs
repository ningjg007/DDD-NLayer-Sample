using System;
using System.Collections.Generic;
using NLayer.Application.CommonModule.DTOs;
using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IRoleGroupService
    {
        RoleGroupDTO Add(RoleGroupDTO roleGroupDTO);

        void Update(RoleGroupDTO roleGroupDTO);

        void Remove(Guid id);

        List<RoleGroupDTO> FindAll();

        RoleGroupDTO FindBy(Guid id);

        IPagedList<RoleGroupDTO> FindBy(string name, int pageNumber, int pageSize);

        List<IdNameDTO> GetUsersIdName(Guid groupId);

        void UpdateGroupUsers(Guid id, List<Guid> users);
    }
}
