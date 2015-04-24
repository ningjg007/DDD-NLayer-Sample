using AutoMapper;
using NLayer.Application.UserSystemModule.DTOs;
using NLayer.Domain.UserSystemModule.Aggregates.UserAgg;

namespace NLayer.Application.UserSystemModule.Converters
{
    public static partial class UserSystemConverters
    {
        public static void InitUserMappers()
        {
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<UserDTO, User>();
        }

        public static User ToModel(this UserDTO dto)
        {
            return Mapper.Map<User>(dto);
        }

        public static UserDTO ToDto(this User model)
        {
            return Mapper.Map<UserDTO>(model);
        }
    }
}
