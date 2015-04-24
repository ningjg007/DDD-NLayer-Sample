using System.Linq;
using AutoMapper;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.MenuAgg;

namespace NLayer.Application.UserSystemModule.Converters
{
    public static partial class UserSystemConverters
    {
        public static void InitMenuMappers()
        {
            Mapper.CreateMap<Menu, MenuDTO>()
                .ForMember(x => x.Permissions, opt => opt.MapFrom(s => s.Permissions.Select(x=>x.ToDto()).ToList()));
            Mapper.CreateMap<MenuDTO, Menu>()
                .ForMember(x => x.Permissions, opt => opt.MapFrom(s => s.Permissions.Select(x => x.ToModel()).ToList()));

            Mapper.CreateMap<Permission, PermissionDTO>();
            Mapper.CreateMap<PermissionDTO, Permission>();
        }

        public static Menu ToModel(this MenuDTO dto)
        {
            return Mapper.Map<Menu>(dto);
        }

        public static MenuDTO ToDto(this Menu model)
        {
            return Mapper.Map<MenuDTO>(model);
        }

        public static Permission ToModel(this PermissionDTO dto)
        {
            return Mapper.Map<Permission>(dto);
        }

        public static PermissionDTO ToDto(this Permission model)
        {
            return Mapper.Map<PermissionDTO>(model);
        }
    }
}
