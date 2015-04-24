using NLayer.Infrastructure.Repository;
using PagedList;

namespace NLayer.Domain.UserSystemModule.Aggregates.MenuAgg
{
    public interface IMenuRepository : IRepository<Menu>
    {
        IPagedList<Menu> FindBy(string module, int pageNumber, int pageSize);
    }
}
