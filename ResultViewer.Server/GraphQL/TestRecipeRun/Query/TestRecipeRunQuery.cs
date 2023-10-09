using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class TestRecipeRunQuery
    {
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the test recipe runs")]
        public async Task<IQueryable<TestRecipeRun>> GetAllTestRecipeRunsAsync([Service] ITestRecipeRunRepository repo)
        {
            // Fetch all TestRecipeRuns from the repository
            var testRecipeRuns = await repo.GetAll();
            return testRecipeRuns.AsQueryable();
        }

        [GraphQLDescription("Returns test recipe run by id")]
        public async Task<TestRecipeRun> GetTestRecipeRunByIdAsync(Guid id, [Service] ITestRecipeRunRepository repo)
        {
            // Fetch a specific TestRecipeRun by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
