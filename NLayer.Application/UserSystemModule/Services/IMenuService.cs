using System;
using System.Collections.Generic;
using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IMenuService
    {
        MenuDTO Add(MenuDTO menuDTO);

        void Update(MenuDTO menuDTO);

        void Remove(Guid id);

        void UpdatePermission(MenuDTO menuDTO);

        MenuDTO FindBy(Guid id);

        IPagedList<MenuDTO> FindBy(string module, string name, int pageNumber, int pageSize);

        List<MenuDTO> FindByModule(string module);
    }
}
