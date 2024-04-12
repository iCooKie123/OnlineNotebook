using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OnlineNotebook.DatabaseConfigurations;

namespace FunctionalTests.Abstractions;

public abstract class ApiTestFixture<TContextService, TContextImplementation, TProgram> : WebApplicationFactory<TProgram>
    where TContextImplementation : DatabaseContext, TContextService
    where TContextService : class, IDatabaseContext
    where TProgram : class
{
    public abstract string DatabaseName { get; }

    public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; } = new();

    public virtual void ConfigureServices(IServiceCollection services)
    {
        var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<TContextImplementation>));

        services.Remove(descriptor);
        services.AddDbContextPool<TContextService, TContextImplementation>(options => options.UseInMemoryDatabase(DatabaseName));

        services.AddSingleton(HttpContextAccessorMock.Object);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) => builder
        .ConfigureAppConfiguration(builder =>
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.AddConfiguration(config);
        })
        .ConfigureServices(services => ConfigureServices(services));
}