using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the MeasurementDataLoader class that extends BatchDataLoader<Guid, Measurement>.
    public class MeasurementDataLoader : BatchDataLoader<Guid, Measurement>
    {
        private readonly IMeasurementRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public MeasurementDataLoader(
            IMeasurementRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadBatchAsync method to load batches of Measurement entities.
        protected override async Task<IReadOnlyDictionary<Guid, Measurement>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            // Call the repository to retrieve Measurement entities using the provided keys.
            var measurements = await _repository.GetManyByIds(keys);

            // Convert the retrieved measurements into a dictionary with Measurement_Id as the key.
            return measurements.ToDictionary(x => x.Measurement_Id);
        }
    }
}
