using NLayer.Infrastructure.Repository;
using PagedList;

namespace NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg
{
    public interface IRoleGroupRepository : IRepository<RoleGroup>
    {
        IPagedList<RoleGroup> FindBy(string name, int pageNumber, int pageSize);
    }
}
