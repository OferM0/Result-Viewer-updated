using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.Repositories
{
    // Define the AROLAccuracyRepository class, which implements IAROLAccuracyRepository and extends GenericRepository<AROLAccuracy>.
    public class AROLAccuracyRepository : GenericRepository<AROLAccuracy>, IAROLAccuracyRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public AROLAccuracyRepository(IDbContextFactory<AppDbContext> contextFactory)
        : base(contextFactory)
        {

        }

        // Retrieve multiple AROLAccuracy entities by their unique IDs.
        public async Task<List<AROLAccuracy>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve AROLAccuracy entities with IDs in the provided list.
                return await context.AROLAccuracies.Where(arol => ids.Contains(arol.Measurement_Id)).ToListAsync();
            }
        }
    }
}
