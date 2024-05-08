using FunctionalTests.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using OnlineNotebook;
using OnlineNotebook.DatabaseConfigurations;

namespace FunctionalTests
{
    public class ApiTestFixture : ApiTestFixture<IDatabaseContext, DatabaseContext, Program>
    {
        public override string DatabaseName => "Tests";

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }
    }
}
