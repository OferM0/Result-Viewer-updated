using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the AROLMeasurementDataLoader class that extends BatchDataLoader<Guid, AROLMeasurement>.
    public class AROLMeasurementDataLoader : BatchDataLoader<Guid, AROLMeasurement>
    {
        private readonly IAROLMeasurementRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public AROLMeasurementDataLoader(
            IAROLMeasurementRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadBatchAsync method to load batches of AROLMeasurement entities.
        protected override async Task<IReadOnlyDictionary<Guid, AROLMeasurement>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            // Call the repository to retrieve AROLMeasurement entities using the provided keys.
            var aROLMeasurements = await _repository.GetManyByIds(keys);

            // Convert the retrieved aROLMeasurements into a dictionary with Measurement_Id as the key.
            return aROLMeasurements.ToDictionary(x => x.Measurement_Id);
        }
    }
}
