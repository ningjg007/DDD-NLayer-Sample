using NLayer.Infrastructure.Repository;
using PagedList;

namespace NLayer.Domain.UserSystemModule.Aggregates.MenuAgg
{
    public interface IPermissionRepository : IRepository<Permission>
    {
    }
}
