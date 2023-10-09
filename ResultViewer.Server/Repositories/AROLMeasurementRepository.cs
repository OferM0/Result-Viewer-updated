using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.Repositories
{
    // Define the AROLMeasurementRepository class, which implements IAROLMeasurementRepository and extends GenericRepository<AROLMeasurement>.
    public class AROLMeasurementRepository : GenericRepository<AROLMeasurement>, IAROLMeasurementRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public AROLMeasurementRepository(IDbContextFactory<AppDbContext> contextFactory)
        : base(contextFactory)
        {

        }

        // Retrieve multiple AROLMeasurement entities by their unique IDs.
        public async Task<List<AROLMeasurement>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve AROLMeasurement entities with IDs in the provided list.
                return await context.AROLMeasurements.Where(arol => ids.Contains(arol.Measurement_Id)).ToListAsync();
            }
        }
    }
}
