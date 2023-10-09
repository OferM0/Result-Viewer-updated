using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    public class WaferRunType : ObjectType<WaferRun>
    {
        // Configure method is used to define the schema for the WaferRunType
        protected override void Configure(IObjectTypeDescriptor<WaferRun> descriptor)
        {
            // Set a description for the WaferRunType
            descriptor.Description("Represents the type of the wafer run.");

            // Define fields for the WaferRunType and set their descriptions
            descriptor
               .Field(r => r.Run_Id)
               .Description("Represents the id for the lot run.");

            descriptor
                .Field(r => r.Wafer_Run_Id)
                .Description("Represents the name for the wafer run."); 

            descriptor
                .Field(r => r.Wafer_Ordinal_Num)
                .Description("Represents the ordinal number of the wafer run.");

            descriptor
                .Field(r => r.Wafer_Start_Time)
                .Description("Represents the starting time of the wafer run.");

            descriptor.Field(r => r.Wafer_End_Time)
                .Description("Represents the ending time of the lot run");

            descriptor.Field(r => r.Conditional_Site_Mode)
                .Description("Represents the conditional site mode of the wafer run");

            // Define a field for lotRun and set its description and resolver
            descriptor
                .Field(p => p.LotRun)
                .ResolveWith<Resolvers>(p => p.GetLotRun(default!, default!))
                .Description("Represents the lotRun object for this wafer run.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching lotRun associated with a WaferRun
            public async Task<LotRun> GetLotRun([Parent] WaferRun waferRun, [Service] LotRunDataLoader dataLoader)
            {
                var lotRun = await dataLoader.LoadAsync(waferRun.Run_Id);
                return lotRun;
            }
        }
    }
}
