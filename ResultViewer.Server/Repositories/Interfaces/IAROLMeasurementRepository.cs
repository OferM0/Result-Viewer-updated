using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{
    // Declare the IAROLMeasurementRepository interface, extending IGenericRepository<AROLMeasurement>.
    public interface IAROLMeasurementRepository : IGenericRepository<AROLMeasurement>
    {
        // Retrieve multiple AROLMeasurement entities by their unique IDs.
        Task<List<AROLMeasurement>> GetManyByIds(IReadOnlyList<Guid> ids);
    }
}
