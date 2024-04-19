using FluentAssertions;
using FunctionalTests.Abstractions;
using OnlineNotebook;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace FunctionalTests
{
    [Collection("Api Collection")]
    public class UsersTests(ApiTestFixture factory) : ApiTest(factory)
    {
        private readonly List<User> TestUsers =
            [
                new User("test.com","test","FirstNameTest","LastNameTest",1,null,null,null,null),
                new User("test.com","test","FirstNameTest","LastNameTest",1,null,null,null,null),
                new User("test.com","test","FirstNameTest","LastNameTest",1,null,null,null,null),
                new User("test.com","test","FirstNameTest","LastNameTest",1,null,null,null,null),

            ];

        [Fact]
        public async Task GetUsersShouldReturnAllUsers()
        {
            await Authenticate();

            await AddTestUsers();

            var response = await Client.GetAsync("users");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var actual = await response.Content.ReadFromJsonAsync<List<UserDTO>>(DefaultOptions);

            actual.Should().NotBeNull();
            actual?.Count.Should().Be(TestUsers.Count);
            actual?[0].Email.Should().Be(TestUsers[0].Email);
        }

        private async Task AddTestUsers()
        {
            await Context.Users.AddRangeAsync(TestUsers);
            await Context.SaveChangesAsync();
        }
    }
}