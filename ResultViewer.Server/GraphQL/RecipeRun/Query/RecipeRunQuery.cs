using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class RecipeRunQuery
    {
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the recipe runs")]
        public async Task<IQueryable<RecipeRun>> GetAllRecipeRunsAsync([Service] IRecipeRunRepository repo)
        {
            // Fetch all RecipeRuns from the repository
            var recipeRuns = await repo.GetAll();
            return recipeRuns.AsQueryable();
        }

        [GraphQLDescription("Returns recipe run by id")]
        public async Task<RecipeRun> GetRecipeRunByIdAsync(Guid id, [Service] IRecipeRunRepository repo)
        {
            // Fetch a specific RecipeRun by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
