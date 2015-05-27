using System;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Infrastructure.Utility.Helper;
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

        public IPagedList<Menu> FindBy(string module, string name, int pageNumber, int pageSize)
        {
            IQueryable<Menu> entities = Table; 
            
            if (name.NotNullOrBlank())
            {
                entities =
                    entities.Where(x => x.Name.Contains(name));
            }

            if (module.NotNullOrBlank())
            {
                entities =
                        entities.Where(x => x.Module == module);
            }

            var totalCountQuery = entities.FutureCount();
            var resultQuery = entities
                .OrderBy(x => x.SortOrder)
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

        public new bool Exists(Menu item)
        {
            IQueryable<Menu> entities = Table;
            entities = entities.Where(x => x.Module == item.Module && x.Name == item.Name);
            if(item.Id != Guid.Empty)
            {
                entities = entities.Where(x => x.Id != item.Id);
            }
            return entities.Any();
        }
    }
}
