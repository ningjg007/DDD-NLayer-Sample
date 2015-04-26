using System;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Infrastructure.Repository;
using PagedList;

namespace NLayer.Domain.UserSystemModule.Aggregates.RoleAgg
{
    public interface IRoleRepository : IRepository<Role>
    {
        IPagedList<Role> FindBy(Guid roleGroupId, string name, int pageNumber, int pageSize);
    }
}
