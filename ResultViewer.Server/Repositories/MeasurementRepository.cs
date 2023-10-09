using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.Repositories
{
    // Define the MeasurementRepository class, which implements IMeasurementRepository and extends GenericRepository<Measurement>.
    public class MeasurementRepository : GenericRepository<Measurement>, IMeasurementRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public MeasurementRepository(IDbContextFactory<AppDbContext> contextFactory)
        : base(contextFactory)
        {

        }

        // Retrieve multiple Measurement entities by their unique IDs.
        public async Task<List<Measurement>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve Measurement entities with IDs in the provided list.
                return await context.Measurements.Where(measurement => ids.Contains(measurement.Measurement_Id)).ToListAsync();
            }
        }

        // Retrieve Measurement entities associated with specific LotRun IDs.
        public async Task<List<Measurement>> GetMeasurementsByLotRunIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve Measurement entities associated with given LotRun IDs.
                return await context.Measurements.Where(measurement => ids.Contains(measurement.Run_Id)).ToListAsync();
            }
        }
    }
}
