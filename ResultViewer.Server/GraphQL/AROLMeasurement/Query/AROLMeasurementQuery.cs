using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class AROLMeasurementQuery
    {
        //[UseOffsetPaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the arol measurement")]
        public async Task<IQueryable<AROLMeasurement>> GetAllAROLMeasurementsAsync([Service] IAROLMeasurementRepository repo)
        {
            // Fetch all AROLMeasurements from the repository
            var arolMeasurements = await repo.GetAll();
            return arolMeasurements.AsQueryable();
        }

        [GraphQLDescription("Returns arol measurement by id")]
        public async Task<AROLMeasurement> GetAROLMeasurementByIdAsync(Guid id, [Service] IAROLMeasurementRepository repo)
        {
            // Fetch a specific AROLMeasuremen by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
