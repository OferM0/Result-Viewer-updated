using ResultViewer.Server.GraphQL.Mutation;
using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{

    // Declare the ILotRunRepository interface, extending IGenericRepository<LotRun>.
    public interface ILotRunRepository : IGenericRepository<LotRun>
    {
        // Retrieve multiple LotRun entities by their unique IDs.
        Task<List<LotRun>> GetManyByIds(IReadOnlyList<Guid> ids);

        // Retrieve LotRun entities associated with specific RecipeRun IDs.
        Task<List<LotRun>> GetLotRunsByRecipeRunIds(IReadOnlyList<Guid> ids);
    }
}
