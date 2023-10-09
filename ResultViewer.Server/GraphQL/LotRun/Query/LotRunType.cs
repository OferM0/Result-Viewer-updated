using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    public class LotRunType : ObjectType<LotRun>
    {
        // Configure method is used to define the schema for the LotRunType
        protected override void Configure(IObjectTypeDescriptor<LotRun> descriptor)
        {
            // Set a description for the LotRunType
            descriptor.Description("Represents the type of the lot run.");

            // Define fields for the LotRunType and set their descriptions
            descriptor
               .Field(r => r.Run_Id)
               .Description("Represents the id of the lot run.");

            descriptor
                .Field(r => r.Lot_Name)
                .Description("Represents the name of the lot run.");

            descriptor
                .Field(r => r.Recipe_Name)
                .Description("Represents the recipe name related to the lot run.");

            descriptor
                .Field(r => r.Lot_Status)
                .Description("Represents the run status of the lot run.");

            descriptor.Field(r => r.Run_Start_Time)
                .Description("Represents the starting time of the lot run.");

            descriptor.Field(r => r.Run_End_Time)
                .Description("Represents the ending time of the lot run.");

            // Define a field for wafer runs and set its description and resolver
            descriptor.Field(p => p.WaferRuns)
                .ResolveWith<Resolvers>(p => p.GetWaferRuns(default, default))
                .Description("Represents a list of available wafer runs for this lot run.");

            // Define a field for measurements and set its description and resolver
            descriptor.Field(p => p.Measurements)
                .ResolveWith<Resolvers>(p => p.GetMeasurements(default, default))
                .Description("Represents a list of available measurements for this lot run.");

            // Define a field for recipe run and set its description and resolver
            descriptor.Field(p => p.RecipeRun)
                .ResolveWith<Resolvers>(p => p.GetRecipeRun(default!, default!))
                .Description("Represents the lotRun object for this wafer run.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching wafer runs associated with a lot run
            public async Task<IQueryable<WaferRun>> GetWaferRuns([Parent] LotRun lotRun, [Service] LotRunWaferRunsDataLoader dataLoader)
            {
                var waferRuns = await dataLoader.LoadAsync(lotRun.Run_Id);
                return waferRuns.AsQueryable();
            }

            // Resolver method for fetching measurements associated with a lot run
            public async Task<IQueryable<Measurement>> GetMeasurements([Parent] LotRun lotRun, [Service] LotRunMeasurementsDataLoader dataLoader)
            {
                var measurements = await dataLoader.LoadAsync(lotRun.Run_Id);
                return measurements.AsQueryable();
            }

            // Resolver method for fetching the recipe run associated with a lot run
            public async Task<RecipeRun> GetRecipeRun([Parent] LotRun lotRun, [Service] RecipeRunDataLoader dataLoader)
            {
                if (lotRun.Recipe_Run_Id == null) return default;
                var recipeRun = await dataLoader.LoadAsync((Guid)lotRun.Recipe_Run_Id);
                return recipeRun;
            }
        }
    }
}
