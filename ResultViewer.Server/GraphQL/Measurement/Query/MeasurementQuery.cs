using ResultViewer.Server.Helpers;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System.Diagnostics.Metrics;


namespace ResultViewer.Server.GraphQL.Query
{
    //Query is responsible for providing the get methods for graphQL
    [ExtendObjectType(Name = "Query")]
    public class MeasurementQuery
    {
        [UseProjection] // Enables field-level projections
        [UseFiltering]  // Enables filtering for query arguments
        [UseSorting]    // Enables sorting for query arguments
        [GraphQLDescription("Returns all the measurement")]
        public async Task<IQueryable<Measurement>> GetAllMeasurementsAsync([Service] IMeasurementRepository repo)
        {
            // Fetch all Measurements from the repository
            var measurements = await repo.GetAll();
            return measurements.AsQueryable();
        }

        [GraphQLDescription("Returns measurement by id")]
        public async Task<Measurement> GetMeasurementByIdAsync(Guid id, [Service] IMeasurementRepository repo)
        {
            // Fetch a specific Measurement by its ID from the repository
            return await repo.GetById(id);
        }
    }
}
