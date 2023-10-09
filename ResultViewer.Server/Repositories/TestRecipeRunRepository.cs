using Microsoft.EntityFrameworkCore;
using ResultViewer.Server.Context;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.Repositories
{
    // Define the TestRecipeRunRepository class, which implements ITestRecipeRunRepository and extends GenericRepository<TestRecipeRun>.
    public class TestRecipeRunRepository : GenericRepository<TestRecipeRun>, ITestRecipeRunRepository
    {
        // Constructor that takes an IDbContextFactory to initialize the repository.
        public TestRecipeRunRepository(IDbContextFactory<AppDbContext> contextFactory)
        : base(contextFactory)
        {

        }

        // Retrieve multiple TestRecipeRun entities by their unique IDs.
        public async Task<List<TestRecipeRun>> GetManyByIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve TestRecipeRun entities with IDs in the provided list.
                return await context.TestRecipeRuns.Where(testRecipeRun => ids.Contains(testRecipeRun.Test_Recipe_Run_Id)).ToListAsync();
            }
        }

        // Retrieve TestRecipeRun entities associated with specific RecipeRun IDs.
        public async Task<List<TestRecipeRun>> GetTestRecipeRunsByRecipeRunIds(IReadOnlyList<Guid> ids)
        {
            using (AppDbContext context = _contextFactory.CreateDbContext())
            {
                // Query the context to retrieve TestRecipeRun entities associated with given RecipeRun IDs.
                return await context.TestRecipeRuns.Where(testRecipeRun => ids.Contains(testRecipeRun.Recipe_Run_Id)).ToListAsync();
            }
        }
    }
}
