using Autofac;
using NLayer.Application.UserSystemModule.Services;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;
using NLayer.Repository.UserSystemModule.Repositories;

namespace NLayer.IocModules
{
    public class UserSystemIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerRequest();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerRequest();
            builder.RegisterType<MenuService>().As<IMenuService>();

            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerRequest();
            builder.RegisterType<RoleService>().As<IRoleService>();

            builder.RegisterType<RoleGroupRepository>().As<IRoleGroupRepository>().InstancePerRequest();
            builder.RegisterType<RoleGroupService>().As<IRoleGroupService>();

            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<AuthService>().As<IAuthService>();
        }
    }
}
