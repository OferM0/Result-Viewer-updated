using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.Repositories
{
    // Define the RecipeRunRepository class, which implements IRecipeRunRepository and extends GenericRepository<RecipeRun>.
    public class RecipeRunRepository : GenericRepository<RecipeRun>, IRecipeRunRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public RecipeRunRepository(IDbContextFactory<AppDbContext> contextFactory)
        : base(contextFactory)
        {

        }

        // Retrieve multiple RecipeRun entities by their unique IDs.
        public async Task<List<RecipeRun>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve RecipeRun entities with IDs in the provided list.
                return await context.RecipeRuns.Where(recipeRun => ids.Contains(recipeRun.Recipe_Run_Id)).ToListAsync();
            }
        }
    }
}
