using System;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Infrastructure.Helper;
using NLayer.Repository.UnitOfWork;
using PagedList;

namespace NLayer.Repository.UserSystemModule.Repositories
{
    public class RoleGroupRepository : SpecificRepositoryBase<RoleGroup>, IRoleGroupRepository
    {
        public RoleGroupRepository(INLayerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IPagedList<RoleGroup> FindBy(string name, int pageNumber, int pageSize)
        {
            IQueryable<RoleGroup> entities = Table; 
            
            if (name.NotNullOrBlank())
            {
                entities =
                    entities.Where(x => x.Name.Contains(name));
            }

            var totalCountQuery = entities.FutureCount();
            var resultQuery = entities
                .OrderBy(x => x.SortOrder)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Future();

            var totalCount = totalCountQuery.Value;
            var result = resultQuery.ToList();

            return new StaticPagedList<RoleGroup>(
                result,
                pageNumber,
                pageSize,
                totalCount);
        }

        public new bool Exists(RoleGroup item)
        {
            IQueryable<RoleGroup> entities = Table;
            entities = entities.Where(x => x.Name == item.Name);
            if (item.Id != Guid.Empty)
            {
                entities = entities.Where(x => x.Id != item.Id);
            }
            return entities.Any();
        }
    }
}
