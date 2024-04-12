using FunctionalTests.Abstractions;
using OnlineNotebook;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;

namespace FunctionalTests
{
    [Collection("Api Collection")]
    public class UnitTest1(ApiTestFixture factory) : ApiTest(factory)
    {
        [Fact]
        public async Task Test1()
        {
            await Client.GetAsync("");
            await Context.AddAsync()
        }

        private static async Task AddTestUsers()
        {
            List<User> users = new()
            {
                new User()
                {
                    Id=1,
                    Email="test.com",
                    Password="test.com"
                }
            };

            await Context.AddRangeAsync(users);
            await Context.SaveChangesAsync();
        }
    }
}