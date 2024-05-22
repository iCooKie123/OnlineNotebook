using OnlineNotebook.Services;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentClassService, StudentClassService>();
            services.AddMemoryCache();
        }
    }
}
