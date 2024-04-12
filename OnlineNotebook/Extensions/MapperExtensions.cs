using AutoMapper;
using DTOs;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace OnlineNotebook.Extensions
{
    public static class MapperExtensions
    {
        public static void AddCustomMappings(this IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserDTO>();
        }
    }
}