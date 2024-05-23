using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineNotebook.Controllers.Helpers;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using OnlineNotebook.Extensions;

namespace OnlineNotebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddCustomMappings();
            });

            var mapper = new Mapper(mapperConfig);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    }
                )
            );

            builder.Services.AddSingleton<IMapper>(mapper);
            builder.Services.AddMediatR(cf =>
                cf.RegisterServicesFromAssembly(typeof(Program).Assembly)
            );
            builder.Services.AddCustomServices();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<IDatabaseContext, DatabaseContext>(options =>
                options.UseSqlServer(connectionString)
            );

            builder
                .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(5),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)
                        ),
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    PolicyName.RequireAdminRole,
                    policy => policy.RequireRole(UserRoles.Admin.ToString())
                );
                options.AddPolicy(
                    PolicyName.RequireAnyRole,
                    policy => policy.RequireAuthenticatedUser()
                );
                options.AddPolicy(
                    PolicyName.RequireStudentRole,
                    policy => policy.RequireRole(UserRoles.Student.ToString())
                );
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
