using Autofac;
using NLayer.Application.UserSystemModule.Services;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Infrastructure.UnitOfWork;
using NLayer.Repository.UnitOfWork;
using NLayer.Repository.UserSystemModule.Repositories;

namespace NLayer.Application.IocModules
{
    public class CommonIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLayerUnitOfWork>().As<INLayerUnitOfWork>().InstancePerRequest();
        }
    }
}
