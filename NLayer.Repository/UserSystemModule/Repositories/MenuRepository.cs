using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Infrastructure.Helper;
using NLayer.Infrastructure.UnitOfWork;
using NLayer.Repository.UnitOfWork;
using PagedList;

namespace NLayer.Repository.UserSystemModule.Repositories
{
    public class MenuRepository : SpecificRepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(INLayerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPagedList<Menu> FindBy(string module, int pageNumber, int pageSize)
        {
            IQueryable<Menu> menuEntities = Table;

            if (module.NotNullOrBlank())
            {
                menuEntities =
                        menuEntities.Where(x => x.Module == module);
            }

            var totalCountQuery = menuEntities.FutureCount();
            var resultQuery = menuEntities
                .OrderByDescending(x => x.Created)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Future();

            var totalCount = totalCountQuery.Value;
            var result = resultQuery.ToList();

            return new StaticPagedList<Menu>(
                result,
                pageNumber,
                pageSize,
                totalCount);
        }
    }
}
