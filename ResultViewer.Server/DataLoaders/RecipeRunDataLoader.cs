using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;
using System;

namespace ResultViewer.Server.DataLoaders
{
    // Define the RecipeRunDataLoader class that extends BatchDataLoader<Guid, RecipeRun>.
    public class RecipeRunDataLoader : BatchDataLoader<Guid, RecipeRun>
    {
        private readonly IRecipeRunRepository _repository;

        // Constructor that initializes the data loader with a repository, batch scheduler, and options.
        public RecipeRunDataLoader(
            IRecipeRunRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _repository = repository; // Store the injected repository instance.
        }

        // Override the LoadBatchAsync method to load batches of RecipeRun entities.
        protected override async Task<IReadOnlyDictionary<Guid, RecipeRun>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            // Call the repository to retrieve RecipeRun entities using the provided keys.
            var recipeRuns = await _repository.GetManyByIds(keys);

            // Convert the retrieved recipeRuns into a dictionary with Recipe_Run_Id as the key.
            return recipeRuns.ToDictionary(x => x.Recipe_Run_Id);
        }
    }
}
