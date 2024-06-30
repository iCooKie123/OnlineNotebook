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
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddCustomMappings();
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

            builder.Services.AddCustomServices(builder.Configuration);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
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
