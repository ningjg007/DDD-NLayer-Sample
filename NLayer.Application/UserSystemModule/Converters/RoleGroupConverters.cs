using AutoMapper;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.RoleGroupAgg;

namespace NLayer.Application.UserSystemModule.Converters
{
    public static partial class UserSystemConverters
    {
        public static void InitRoleGroupMappers()
        {
            Mapper.CreateMap<RoleGroup, RoleGroupDTO>();
            Mapper.CreateMap<RoleGroupDTO, RoleGroup>();
        }

        public static RoleGroup ToModel(this RoleGroupDTO dto)
        {
            return Mapper.Map<RoleGroup>(dto);
        }

        public static RoleGroupDTO ToDto(this RoleGroup model)
        {
            return Mapper.Map<RoleGroupDTO>(model);
        }
    }
}
