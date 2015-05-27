using System;
using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IMenuService
    {
        MenuDTO Add(MenuDTO menuDTO);

        void Update(MenuDTO menuDTO);

        void Remove(Guid id);

        MenuDTO FindBy(Guid id);

        IPagedList<MenuDTO> FindBy(string module, string name, int pageNumber, int pageSize);
    }
}
