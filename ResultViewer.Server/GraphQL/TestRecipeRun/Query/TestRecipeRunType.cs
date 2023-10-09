using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{        
    public class TestRecipeRunType : ObjectType<TestRecipeRun>
    {
        // Configure method is used to define the schema for the TestRecipeRunType
        protected override void Configure(IObjectTypeDescriptor<TestRecipeRun> descriptor)
        {
            // Set a description for the TestRecipeRunType
            descriptor.Description("Represents the type of the test recipe run.");

            // Define fields for the TestRecipeRunType and set their descriptions
            descriptor
               .Field(r => r.Test_Recipe_Run_Id)
               .Description("Represents the id of the test recipe run.");

            descriptor
                .Field(r => r.Recipe_Run_Id)
                .Description("Represents the id of the recipe run.");

            // Define a field for RecipeRun and set its description and resolver
            descriptor
                .Field(r => r.RecipeRun)
                .ResolveWith<Resolvers>(p => p.GetRecipeRun(default!, default!))
                .Description("Represents the associated recipe run.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching RecipeRun associated with a TestRecipeRunType
            public async Task<RecipeRun> GetRecipeRun([Parent] TestRecipeRun testRecipeRun, [Service] RecipeRunDataLoader dataLoader)
            {
                var recipeRun = await dataLoader.LoadAsync(testRecipeRun.Recipe_Run_Id);
                return recipeRun;
            }
        }
    }
}