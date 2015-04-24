using System.Data.Entity;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Repository.UnitOfWork;

namespace NLayer.Repository.UserSystemModule.Repositories
{
    public class RoleGroupRepository : SpecificRepositoryBase<RoleGroup>, IRoleGroupRepository
    {
        public RoleGroupRepository(INLayerUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
