using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class AROLAccuracyQuery
    {
        //[UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the arol accuracies")]
        public async Task<IQueryable<AROLAccuracy>> GetAllAROLAccuraciesAsync([Service] IAROLAccuracyRepository repo)
        {
            // Fetch all AROLAccuracies from the repository
            var arolAccuracies = await repo.GetAll();
            return arolAccuracies.AsQueryable();
        }

        [GraphQLDescription("Returns arol accuracy by id")]
        public async Task<AROLAccuracy> GetAROLAccuracyByIdAsync(Guid id, [Service] IAROLAccuracyRepository repo)
        {
            // Fetch a specific AROLAccuracy by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
