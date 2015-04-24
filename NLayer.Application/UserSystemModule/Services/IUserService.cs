using NLayer.Application.UserSystemModule.DTOs;
using PagedList;

namespace NLayer.Application.UserSystemModule.Services
{
    public interface IUserService
    {
        UserDTO Add(UserDTO userDTO);

        void Update(UserDTO userDTO);

        void Remove(UserDTO userDTO);

        IPagedList<UserDTO> FindBy(int pageNumber, int pageSize);
    }
}
