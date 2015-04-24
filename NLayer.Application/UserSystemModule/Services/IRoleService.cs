using System.Collections.Generic;
using NLayer.Application.UserSystemModule.DTOs;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IRoleService
    {
        RoleDTO Add(RoleDTO roleDTO);

        void Update(RoleDTO roleDTO);

        void Remove(RoleDTO roleDTO);

        List<RoleDTO> FindAll();
    }
}
