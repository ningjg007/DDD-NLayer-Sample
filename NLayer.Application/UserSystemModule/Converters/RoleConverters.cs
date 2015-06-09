using AutoMapper;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.RoleAgg;

namespace NLayer.Application.UserSystemModule.Converters
{
    public static partial class UserSystemConverters
    {
        public static void InitRoleMappers()
        {
            Mapper.CreateMap<Role, RoleDTO>();

            Mapper.CreateMap<RoleDTO, Role>();
        }

        public static Role ToModel(this RoleDTO dto)
        {
            return Mapper.Map<Role>(dto);
        }

        public static RoleDTO ToDto(this Role model)
        {
            return Mapper.Map<RoleDTO>(model);
        }
    }
}
