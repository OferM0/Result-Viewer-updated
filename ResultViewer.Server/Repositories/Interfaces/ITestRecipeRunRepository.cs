using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{
    // Declare the ITestRecipeRunRepository interface, extending IGenericRepository<TestRecipeRun>.
    public interface ITestRecipeRunRepository : IGenericRepository<TestRecipeRun>
    {    
        // Retrieve multiple TestRecipeRun entities by their unique IDs.
        Task<List<TestRecipeRun>> GetManyByIds(IReadOnlyList<Guid> ids);

        // Retrieve TestRecipeRun entities associated with specific RecipeRun IDs.
        Task<List<TestRecipeRun>> GetTestRecipeRunsByRecipeRunIds(IReadOnlyList<Guid> ids);
    }
}
