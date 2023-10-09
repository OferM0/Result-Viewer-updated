namespace ResultViewer.Server.GraphQL.Mutation
{
    public class LotRuntInputType : InputObjectType<LotRunInput>
    {
        // Configure method is used to define the schema for the LotRuntInputType
        protected override void Configure(IInputObjectTypeDescriptor<LotRunInput> descriptor)
        {
            // Set a description for the LotRuntInput
            descriptor.Description("Represents the input to create and update for a result.");

            // Define fields for the LotRuntInputType and set their descriptions
            descriptor
                .Field( r=> r.Lot_Name)
                .Description("Represents the name for the lot run.");

            descriptor
                .Field(r => r.Recipe_Name)
                .Description("Represents the recipe name related to the lot run.");

            descriptor
                .Field(r => r.Lot_Status)
                .Description("Represents the run status of the lot run.");

            descriptor.Field(r => r.Run_Start_Time)
                .Description("Represents the starting time of the lot run");
            
            descriptor.Field(r => r.Run_End_Time)
                .Description("Represents the ending time of the lot run");

            base.Configure(descriptor);
        }
    }
}
