using AutoMapper;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Queries;

namespace OnlineNotebook.Extensions
{
    public static class MapperExtensions
    {
        public static void AddCustomMappings(this IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserDTO>();
            config.CreateMap<StudentClass, GetStudentClassesQueryResponse>();
        }
    }
}
