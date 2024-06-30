using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using OnlineNotebook.Services;
using OnlineNotebook.Services.Abstractions;

namespace OnlineNotebook.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomServices(
            this IServiceCollection services,
            ConfigurationManager configuration
        )
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentClassService, StudentClassService>();
            services.AddMemoryCache();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
                options.UseSqlServer(connectionString)
            );

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JwtSettings:Audience"],
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(5),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)
                        ),
                    };
                });

            services
                .AddAuthorizationBuilder()
                .AddPolicy(
                    PolicyName.RequireAdminRole,
                    policy => policy.RequireRole(UserRoles.Admin.ToString())
                )
                .AddPolicy(PolicyName.RequireAnyRole, policy => policy.RequireAuthenticatedUser())
                .AddPolicy(
                    PolicyName.RequireStudentRole,
                    policy => policy.RequireRole(UserRoles.Student.ToString())
                );
        }
    }
}
