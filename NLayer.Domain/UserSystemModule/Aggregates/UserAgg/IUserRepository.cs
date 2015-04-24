using NLayer.Infrastructure.Repository;
using PagedList;

namespace NLayer.Domain.UserSystemModule.Aggregates.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        IPagedList<User> FindBy(int pageNumber, int pageSize);
    }
}
