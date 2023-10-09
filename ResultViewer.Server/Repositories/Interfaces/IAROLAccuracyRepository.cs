using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{
    // Declare the IAROLAccuracyRepository interface, extending IGenericRepository<AROLAccuracy>.
    public interface IAROLAccuracyRepository : IGenericRepository<AROLAccuracy>
    {
        // Retrieve multiple AROLAccuracy entities by their unique IDs.
        Task<List<AROLAccuracy>> GetManyByIds(IReadOnlyList<Guid> ids);
    }
}
