using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the LotRunWaferRunsDataLoader class that extends GroupedDataLoader<Guid, WaferRun>.
    public class LotRunWaferRunsDataLoader : GroupedDataLoader<Guid, WaferRun>
    {
        private readonly IWaferRunRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public LotRunWaferRunsDataLoader(
            IWaferRunRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadGroupedBatchAsync method to load grouped batches of WaferRuns entities.
        protected override async Task<ILookup<Guid, WaferRun>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            // Call the repository to retrieve WaferRun entities using the provided keys.
            var waferRuns = await _repository.GetWaferRunsByLotRunIds(keys);

            // Convert the retrieved waferRuns into a grouped lookup with Run_Id as the key.
            return waferRuns.ToLookup(waferRun => waferRun.Run_Id);
        }
    }
}
