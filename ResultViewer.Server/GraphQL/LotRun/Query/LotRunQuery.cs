using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class LotRunQuery
    {
        //[UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the lot runs")]
        public async Task<IQueryable<LotRun>> GetAllLotRunsAsync([Service] ILotRunRepository repo)
        {
            // Fetch all lot runs from the repository
            var lotRuns = await repo.GetAll();
            return lotRuns.AsQueryable();
        }

        [GraphQLDescription("Returns lot run by id")]
        public async Task<LotRun> GetLotRunByIdAsync(Guid id, [Service] ILotRunRepository repo)
        {
            // Fetch a specific lot run by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
