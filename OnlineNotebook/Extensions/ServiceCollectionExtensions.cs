using OnlineNotebook.Services.Abstractions;
using OnlineNotebook.Services;

namespace OnlineNotebook.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}