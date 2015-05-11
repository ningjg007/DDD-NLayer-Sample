using System;
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
    public class PermissionRepository : SpecificRepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(INLayerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public new bool Exists(Permission item)
        {
            IQueryable<Permission> entities = Table;
            entities = entities.Where(x => x.Menu == item.Menu && x.Name == item.Name);
            if(item.Id != Guid.Empty)
            {
                entities = entities.Where(x => x.Id != item.Id);
            }
            return entities.Any();
        }
    }
}
