using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using OnlineNotebook.Controllers.Helpers;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.Extensions;
using OnlineNotebook.Services;
using OnlineNotebook.Services.Abstractions;

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
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IMapper>(mapper);
            builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddCustomServices();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

            //if (!builder.Environment.IsProduction())
            //{
            //    using (var scope = app.Services.CreateScope())
            //    {
            //        var services = scope.ServiceProvider;

            //        var context = services.GetRequiredService<DatabaseContext>();

            //        context.Database.Migrate();
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(builder => builder.WithOrigins(
            [
                "http://localhost:5173"
            ])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            app.UseExceptionHandlingMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}