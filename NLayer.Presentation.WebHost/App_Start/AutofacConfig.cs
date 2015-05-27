using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Configuration;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Practices.ServiceLocation;
using NLayer.Application.Managers;
using NLayer.Infrastructure.Authorize;
using NLayer.IocModules;
using NLayer.Presentation.WebHost.Controllers;
using NLayer.Presentation.WebHost.Helper;

namespace NLayer.Presentation.WebHost.App_Start
{
    public static class AutofacConfig
    {
        public static IContainer Container { get; private set; }

        public static void Config()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));

            var executingAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterApiControllers(executingAssembly)
                .InstancePerRequest();//注册api容器的实现

            builder.RegisterControllers(executingAssembly)
                .InstancePerRequest();//注册mvc容器的实现
            builder.RegisterModelBinderProvider();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            // 系统定义的模块
            builder.RegisterModule<CommonIocModule>();
            builder.RegisterModule<UserSystemIocModule>();

            //builder.RegisterType<NLayerServiceResolver>().As<IServiceResolver>();

            //builder.RegisterType<BaseAuthorizeController>().PropertiesAutowired();

            //builder.Register(c => new BaseAuthorizeController()).OnActivated(e =>
            //{
            //    e.Instance.AuthorizeManager = e.Context.Resolve<IAuthorizeManager>();
            //}).InstancePerRequest();

            //builder.Register(c => new BaseAuthorizeController() { AuthorizeManager = c.Resolve<IAuthorizeManager>() }).InstancePerRequest();

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));//注册mvc容器

            //var csl = new AutofacServiceLocator(Container);
            //ServiceLocator.SetLocatorProvider(() => csl);
        }
    }
}