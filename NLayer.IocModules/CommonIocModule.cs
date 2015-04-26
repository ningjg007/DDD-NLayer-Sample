using Autofac;
using NLayer.Repository.UnitOfWork;

namespace NLayer.IocModules
{
    public class CommonIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLayerUnitOfWork>().As<INLayerUnitOfWork>().InstancePerRequest();
        }
    }
}
