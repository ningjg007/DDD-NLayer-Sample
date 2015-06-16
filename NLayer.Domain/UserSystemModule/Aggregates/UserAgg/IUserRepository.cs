using System;
using NLayer.Infrastructure.Repository;
using PagedList;

namespace NLayer.Domain.UserSystemModule.Aggregates.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {
        IPagedList<User> FindBy(string name, int pageNumber, int pageSize);

        bool ExistsLoginName(User item);

        bool ExistsEmail(User item);
    }
}
