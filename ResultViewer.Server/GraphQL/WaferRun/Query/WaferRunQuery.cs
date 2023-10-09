using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class WaferRunQuery
    {
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the wafer runs")]
        public async Task<IQueryable<WaferRun>> GetAllWaferRunsAsync([Service] IWaferRunRepository repo)
        {
            // Fetch all WaferRuns from the repository
            var waferRuns = await repo.GetAll();
            return waferRuns.AsQueryable();
        }

        [GraphQLDescription("Returns wafer run by id")]
        public async Task<WaferRun> GetWaferRunByIdAsync(Guid id, [Service] IWaferRunRepository repo)
        {
            // Fetch a specific WaferRun by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
