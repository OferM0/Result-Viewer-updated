using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{
    // Declare the IWaferRunRepository interface, extending IGenericRepository<WaferRun>.
    public interface IWaferRunRepository : IGenericRepository<WaferRun>
    {
        // Retrieve multiple WaferRun entities by their unique IDs.
        Task<List<WaferRun>> GetManyByIds(IReadOnlyList<Guid> ids);

        // Retrieve WaferRun entities associated with specific LotRun IDs.
        Task<List<WaferRun>> GetWaferRunsByLotRunIds(IReadOnlyList<Guid> ids);
    }
}
