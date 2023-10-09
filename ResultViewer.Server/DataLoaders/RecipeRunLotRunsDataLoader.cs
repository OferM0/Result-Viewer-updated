using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the RecipeRunLotRunsDataLoader class that extends GroupedDataLoader<Guid, LotRun>.
    public class RecipeRunLotRunsDataLoader : GroupedDataLoader<Guid, LotRun>
    {
        private readonly ILotRunRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public RecipeRunLotRunsDataLoader(
            ILotRunRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadGroupedBatchAsync method to load grouped batches of LotRun entities.
        protected override async Task<ILookup<Guid, LotRun>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            // Call the repository to retrieve LotRun entities using the provided keys.
            var lotRuns = await _repository.GetLotRunsByRecipeRunIds(keys);

            // Convert the retrieved lotRuns into a grouped lookup with Recipe_Run_Id as the key.
            // If Recipe_Run_Id is null, use default value as key to represent unassociated LotRuns.
            return lotRuns.ToLookup(lotRun => lotRun.Recipe_Run_Id == null ? default : (Guid)lotRun.Recipe_Run_Id);
        }
    }
}
