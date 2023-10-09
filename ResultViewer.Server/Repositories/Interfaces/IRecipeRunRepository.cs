using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{
    // Declare the IRecipeRunRepository interface, extending IGenericRepository<RecipeRun>.
    public interface IRecipeRunRepository : IGenericRepository<RecipeRun>
    {
        // Retrieve multiple RecipeRun entities by their unique IDs.
        Task<List<RecipeRun>> GetManyByIds(IReadOnlyList<Guid> ids);
    }
}
