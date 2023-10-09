using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    public class RecipeRunType : ObjectType<RecipeRun>
    {
        // Configure method is used to define the schema for the RecipeRunType
        protected override void Configure(IObjectTypeDescriptor<RecipeRun> descriptor)
        {
            // Set a description for the RecipeRunType
            descriptor.Description("Represents the type of the recipe run.");

            // Define fields for the RecipeRunType and set their descriptions
            descriptor
               .Field(r => r.Recipe_Run_Id)
               .Description("Represents the id of the recipe run.");

            descriptor
                .Field(r => r.Recipe_Name)
                .Description("Represents the name of the recipe run.");

            // Define a field for LotRuns list and set its description and resolver
            descriptor
                .Field(r => r.LotRuns)
                .ResolveWith<Resolvers>(p => p.GetLotRuns(default!, default!))
                .Description("Represents the lot runs list related to the recipe run.");

            // Define a field for TestRecipeRuns list and set its description and resolver
            descriptor
                .Field(r => r.TestRecipeRuns)
                .ResolveWith<Resolvers>(p => p.GetTestRecipeRuns(default!, default!))
                .Description("Represents the lot runs list related to the recipe run.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching LotRuns associated with a RecipeRun
            public async Task<IQueryable<LotRun>> GetLotRuns([Parent] RecipeRun recipeRun, [Service] RecipeRunLotRunsDataLoader dataLoader)
            {
                var lotRuns = await dataLoader.LoadAsync(recipeRun.Recipe_Run_Id);
                return lotRuns.AsQueryable();
            }

            // Resolver method for fetching TestRecipeRuns associated with a RecipeRun
            public async Task<IQueryable<TestRecipeRun>> GetTestRecipeRuns([Parent] RecipeRun recipeRun, [Service] RecipeRunTestRecipeRunsDataLoader dataLoader)
            {
                var testRecipeRuns = await dataLoader.LoadAsync(recipeRun.Recipe_Run_Id);
                return testRecipeRuns.AsQueryable();
            }
        }
    }
}
