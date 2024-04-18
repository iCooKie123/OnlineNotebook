using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineNotebook;
using OnlineNotebook.Controllers;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalTests.Abstractions;

public abstract class ApiTests<TApiTestFixture, TContextService, TContextImplementation, TProgram> : IDisposable
    where TApiTestFixture : ApiTestFixture<TContextService, TContextImplementation, TProgram>
    where TContextImplementation : DatabaseContext, TContextService
    where TContextService : class, IDatabaseContext
    where TProgram : class
{
    public ApiTests(TApiTestFixture factory)
    {
        Factory = factory;

        Client = Factory.CreateDefaultClient();
        Scope = Factory.Services.CreateScope();
        Context = Services.GetRequiredService<TContextImplementation>();

        Context.Database.EnsureCreated();

        Factory.HttpContextAccessorMock
            .SetupGet(m => m.HttpContext)
            .Returns(HttpContext);
    }

    public HttpClient Client { get; }
    public TContextImplementation Context { get; }
    public TApiTestFixture Factory { get; }
    public HttpContext HttpContext { get; } = new DefaultHttpContext();
    public IServiceScope Scope { get; }
    public IServiceProvider Services => Scope.ServiceProvider;

    public virtual void Dispose()
    {
        Context.Database.EnsureDeleted();
        Scope?.Dispose();
        GC.SuppressFinalize(this);
    }
}

public abstract class ApiTest : ApiTests<ApiTestFixture, IDatabaseContext, DatabaseContext, Program>
{
    public IMapper Mapper { get; }

    public JsonSerializerOptions DefaultOptions { get; }

    protected ApiTest(ApiTestFixture factory) : base(factory)
    {
        Mapper = Services.GetRequiredService<IMapper>();

        DefaultOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
    }

    public async Task Authenticate()
    {
        var user = new User("test", "test", "test", "test", 1);
        {
        }

        Context.Users.Add(user);
        Context.SaveChanges();

        var request = new
        {
            user.Email,
            user.Password
        };

        var result = await Client.PutAsJsonAsync($"{UserController.Route}/login", request);
        var token = await result.Content.ReadAsStringAsync();

        Client.DefaultRequestHeaders.Authorization = new("Bearer", token);

        Context.Users.Remove(user);
        Context.SaveChanges();
    }
}