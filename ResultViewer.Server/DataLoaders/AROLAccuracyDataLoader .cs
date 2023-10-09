using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the AROLAccuracyDataLoader class that extends BatchDataLoader<Guid, AROLAccuracy>.
    public class AROLAccuracyDataLoader : BatchDataLoader<Guid, AROLAccuracy>
    {
        private readonly IAROLAccuracyRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public AROLAccuracyDataLoader(
            IAROLAccuracyRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository;  // Store the injected repository instance.
        }

        // Override the LoadBatchAsync method to load batches of AROLAccuracy entities.
        protected override async Task<IReadOnlyDictionary<Guid, AROLAccuracy>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            // Call the repository to retrieve AROLAccuracy entities using the provided keys.
            var aROLAccuracies = await _repository.GetManyByIds(keys);

            // Convert the retrieved AROLAccuracies into a dictionary with Measurement_Id as the key.
            return aROLAccuracies.ToDictionary(x => x.Measurement_Id);
        }
    }
}
