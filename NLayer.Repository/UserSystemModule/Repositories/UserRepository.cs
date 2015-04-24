using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Repository.UnitOfWork;
using PagedList;

namespace NLayer.Repository.UserSystemModule.Repositories
{
    public class UserRepository : SpecificRepositoryBase<User>, IUserRepository
    {
        public UserRepository(INLayerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPagedList<User> FindBy(int pageNumber, int pageSize)
        {
            IQueryable<User> userEntities = Table;

            var totalCountQuery = userEntities.FutureCount();
            var resultQuery = userEntities
                .OrderByDescending(x => x.Created)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Future();

            var totalCount = totalCountQuery.Value;
            var result = resultQuery.ToList();

            return new StaticPagedList<User>(
                result,
                pageNumber,
                pageSize,
                totalCount);
        }
    }
}
