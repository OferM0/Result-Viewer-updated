using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ResultViewer.Server.Context;
using ResultViewer.Server.GraphQL.Mutation;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System.Data;

namespace ResultViewer.Server.Repositories
{
    // Define the LotRunRepository class, which implements ILotRunRepository and extends GenericRepository<LotRun>.
    public class LotRunRepository : GenericRepository<LotRun>, ILotRunRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public LotRunRepository(IDbContextFactory<AppDbContext> contextFactory)
            : base(contextFactory)  // Call the constructor of the base class.
        {

        }

        // Retrieve multiple LotRun entities by their unique IDs.
        public async Task<List<LotRun>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve LotRun entities with IDs in the provided list.
                return await context.LotRuns
                    .Where(lotRun => ids.Contains(lotRun.Run_Id))
                    .ToListAsync();
            }
        }

        // Retrieve LotRun entities associated with specific RecipeRun IDs.
        public async Task<List<LotRun>> GetLotRunsByRecipeRunIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve LotRun entities associated with given RecipeRun IDs.
                return await context.LotRuns
                    .Where(lotRun => ids.Contains((Guid)lotRun.Recipe_Run_Id))
                    .ToListAsync();
            }
        }
    }

}
