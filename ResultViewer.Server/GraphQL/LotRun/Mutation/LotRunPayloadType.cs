namespace ResultViewer.Server.GraphQL.Mutation
{
    public class LotRunPayloadType : ObjectType<LotRunPayload>
    {
        // Configure method is used to define the schema for the LotRunPayloadType
        protected override void Configure(IObjectTypeDescriptor<LotRunPayload> descriptor)
        {
            // Set a description for the LotRunPayload
            descriptor.Description("Represents the payload to return for a created or updated lot run.");

            // Define fields for the LotRunPayloadType and set their descriptions
            descriptor
                .Field(r => r.LotRun)
                .Description("Represents the lot run object.");

            base.Configure(descriptor);
        }
    }
}
