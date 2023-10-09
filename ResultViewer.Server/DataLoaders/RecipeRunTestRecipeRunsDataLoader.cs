using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the RecipeRunTestRecipeRunsDataLoader class that extends GroupedDataLoader<Guid, TestRecipeRun>.
    public class RecipeRunTestRecipeRunsDataLoader : GroupedDataLoader<Guid, TestRecipeRun>
    {
        private readonly ITestRecipeRunRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public RecipeRunTestRecipeRunsDataLoader(
            ITestRecipeRunRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadGroupedBatchAsync method to load grouped batches of TestRecipeRun entities.
        protected override async Task<ILookup<Guid, TestRecipeRun>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            // Call the repository to retrieve TestRecipeRun entities using the provided keys.
            var testRecipeRuns = await _repository.GetTestRecipeRunsByRecipeRunIds(keys);

            // Convert the retrieved testRecipeRuns into a grouped lookup with Recipe_Run_Id as the key.
            return testRecipeRuns.ToLookup(testRecipeRun => testRecipeRun.Recipe_Run_Id);
        }
    }
}
