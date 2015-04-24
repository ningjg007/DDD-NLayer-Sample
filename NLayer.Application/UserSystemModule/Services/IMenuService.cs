using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IMenuService
    {
        MenuDTO Add(MenuDTO menuDTO);

        void Update(MenuDTO menuDTO);

        void Remove(MenuDTO menuDTO);

        IPagedList<MenuDTO> FindBy(string module, int pageNumber, int pageSize);
    }
}
