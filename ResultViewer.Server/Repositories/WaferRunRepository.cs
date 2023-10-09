using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.GraphQL.Mutation;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System.Data;

namespace ResultViewer.Server.Repositories
{
    // Define the WaferRunRepository class, which implements IWaferRunRepository and extends GenericRepository<WaferRun>.
    public class WaferRunRepository : GenericRepository<WaferRun>, IWaferRunRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public WaferRunRepository(IDbContextFactory<AppDbContext> contextFactory)
        : base(contextFactory)
        {

        }

        // Retrieve multiple WaferRun entities by their unique IDs.
        public async Task<List<WaferRun>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve WaferRun entities with IDs in the provided list.
                return await context.WaferRuns.Where(waferRun => ids.Contains(waferRun.Wafer_Run_Id)).ToListAsync();
            }
        }

        // Retrieve WaferRun entities associated with specific LotRun IDs.
        public async Task<List<WaferRun>> GetWaferRunsByLotRunIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve WaferRun entities associated with given LotRun IDs.
                return await context.WaferRuns.Where(waferRun => ids.Contains(waferRun.Run_Id)).ToListAsync();
            }
        }
    }
}
