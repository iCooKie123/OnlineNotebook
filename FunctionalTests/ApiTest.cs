using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineNotebook;
using OnlineNotebook.DatabaseConfigurations;
using System;
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
}