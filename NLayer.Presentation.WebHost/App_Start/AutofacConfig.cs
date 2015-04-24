using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.Practices.ServiceLocation;
using NLayer.Application.IocModules;

namespace NLayer.Presentation.WebHost.App_Start
{
    public static class AutofacConfig
    {
        public static IContainer Container { get; private set; }

        public static void Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());//注册api容器的实现

            builder.RegisterControllers(Assembly.GetExecutingAssembly());//注册mvc容器的实现

            // 系统定义的模块
            builder.RegisterModule<CommonIocModule>();
            builder.RegisterModule<UserSystemIocModule>();

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));//注册mvc容器
        }
    }
}