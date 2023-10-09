using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the LotRunMeasurementsDataLoader class that extends GroupedDataLoader<Guid, Measurement>.
    public class LotRunMeasurementsDataLoader : GroupedDataLoader<Guid, Measurement>
    {
        private readonly IMeasurementRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public LotRunMeasurementsDataLoader(
            IMeasurementRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadGroupedBatchAsync method to load grouped batches of Measurement entities.
        protected override async Task<ILookup<Guid, Measurement>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            // Call the repository to retrieve Measurement entities using the provided keys.
            var measurements = await _repository.GetMeasurementsByLotRunIds(keys);

            // Convert the retrieved measurements into a grouped lookup with Run_Id as the key.
            return measurements.ToLookup(measurement => measurement.Run_Id);
        }
    }
}
