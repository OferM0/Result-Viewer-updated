using ResultViewer.Server.Models;

namespace ResultViewer.Server.Repositories.Interfaces
{
    // Declare the IMeasurementRepository interface, extending IGenericRepository<Measurement>.
    public interface IMeasurementRepository : IGenericRepository<Measurement>
    {
        // Retrieve multiple Measurement entities by their unique IDs.
        Task<List<Measurement>> GetManyByIds(IReadOnlyList<Guid> ids);

        // Retrieve Measurement entities associated with specific LotRun IDs.
        Task<List<Measurement>> GetMeasurementsByLotRunIds(IReadOnlyList<Guid> ids);
    }
}
