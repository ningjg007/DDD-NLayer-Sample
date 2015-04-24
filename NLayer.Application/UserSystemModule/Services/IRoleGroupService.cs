using System.Collections.Generic;
using NLayer.Application.UserSystemModule.DTOs;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IRoleGroupService
    {
        RoleGroupDTO Add(RoleGroupDTO roleGroupDTO);

        void Update(RoleGroupDTO roleGroupDTO);

        void Remove(RoleGroupDTO roleGroupDTO);

        List<RoleGroupDTO> FindAll();
    }
}
