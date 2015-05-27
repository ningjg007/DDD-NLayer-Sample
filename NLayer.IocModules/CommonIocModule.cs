using Autofac;
using NLayer.Application.Managers;
using NLayer.Infrastructure.Authorize;
using NLayer.Infrastructure.Utility.Caching;
using NLayer.Repository.UnitOfWork;

namespace NLayer.IocModules
{
    public class CommonIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLayerUnitOfWork>().As<INLayerUnitOfWork>().InstancePerRequest();

            builder.RegisterType<LocalCacheManager>().As<ICacheManager>().InstancePerRequest();

            builder.RegisterType<AuthorizeManager>().As<IAuthorizeManager>().InstancePerRequest();

            builder.RegisterType<AuthorizeFilter>().PropertiesAutowired();
        }
    }
}
