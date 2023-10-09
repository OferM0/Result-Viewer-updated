using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.GraphQL.Common.Mutations;
using ResultViewer.Server.GraphQL.Common.Queries;
using ResultViewer.Server.GraphQL.Mutation;
using ResultViewer.Server.GraphQL.Query;
using ResultViewer.Server.Helpers;
using ResultViewer.Server.Repositories;
using ResultViewer.Server.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configure database context factory and connection string.
builder.Services.AddDbContextFactory<AppDbContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

// Register repositories and data loaders.
builder.Services.AddScoped<IWaferRunRepository, WaferRunRepository>();
builder.Services.AddScoped<ILotRunRepository, LotRunRepository>();
builder.Services.AddScoped<IRecipeRunRepository, RecipeRunRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IAROLMeasurementRepository, AROLMeasurementRepository>();
builder.Services.AddScoped<IAROLAccuracyRepository, AROLAccuracyRepository>();
builder.Services.AddScoped<ITestRecipeRunRepository, TestRecipeRunRepository>();
builder.Services.AddScoped<LotRunDataLoader>();
builder.Services.AddScoped<LotRunMeasurementsDataLoader>();
builder.Services.AddScoped<LotRunWaferRunsDataLoader>();
builder.Services.AddScoped<AROLMeasurementDataLoader>();
builder.Services.AddScoped<AROLAccuracyDataLoader>();
builder.Services.AddScoped<MeasurementDataLoader>();
builder.Services.AddScoped<RecipeRunDataLoader>();
builder.Services.AddScoped<RecipeRunLotRunsDataLoader>();
builder.Services.AddScoped<RecipeRunTestRecipeRunsDataLoader>();

// Configure AutoMapper for object mapping.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configure GraphQL server.
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<LotRunQuery>()
    .AddTypeExtension<LotRunMutation>()
    .AddTypeExtension<WaferRunQuery>()
    .AddTypeExtension<RecipeRunQuery>()
    .AddTypeExtension<TestRecipeRunQuery>()
    .AddTypeExtension<MeasurementQuery>()
    .AddTypeExtension<AROLMeasurementQuery>()
    .AddTypeExtension<AROLAccuracyQuery>()
    .AddType<LotRunType>()
    .AddType<WaferRunType>()
    .AddType<RecipeRunType>()
    .AddType<TestRecipeRunType>()
    .AddType<LotRuntInputType>()
    .AddType<LotRunPayloadType>()
    .AddType<MeasurementType>()
    .AddType<AROLMeasurementType>()
    .AddType<AROLAccuracyType>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();

// Configure error message helper using JSON file.
builder.Configuration.AddJsonFile("Helpers\\errorMessageHelper.json");

// Retrieve the configuration values for error messages.
var config = builder.Configuration.Get<ErrorMessageHelper>();

// Register the configuration with Dependency Injection (DI).
builder.Services.AddSingleton(config);

// Configure output caching.
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromDays(1);  // Set the default cache expiration time.
});

var app = builder.Build();  // Build the application.

app.UseOutputCache();  // Use output caching.

app.UseHttpsRedirection();  // Enable HTTPS redirection.
app.MapGraphQL("/graphql");  // Map the GraphQL endpoint.

app.Run();  // Start the application.