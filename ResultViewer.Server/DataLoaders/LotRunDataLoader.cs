using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the LotRunDataLoader class that extends BatchDataLoader<Guid, LotRun>.
    public class LotRunDataLoader : BatchDataLoader<Guid, LotRun>
    {
        private readonly ILotRunRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public LotRunDataLoader(
            ILotRunRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository;  // Store the injected repository instance.
        }

        // Override the LoadBatchAsync method to load batches of LotRun entities.
        protected override async Task<IReadOnlyDictionary<Guid, LotRun>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            // Call the repository to retrieve LotRun entities using the provided keys.
            var lotRuns = await _repository.GetManyByIds(keys);

            // Convert the retrieved lotRuns into a dictionary with Run_Id as the key.
            return lotRuns.ToDictionary(x => x.Run_Id);
        }
    }
}
