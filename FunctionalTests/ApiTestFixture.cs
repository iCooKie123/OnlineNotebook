using FunctionalTests.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using OnlineNotebook;
using OnlineNotebook.DatabaseConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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