using System.Data.Entity;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Repository.UnitOfWork;

namespace NLayer.Repository.UserSystemModule.Repositories
{
    public class RoleRepository : SpecificRepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(INLayerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
