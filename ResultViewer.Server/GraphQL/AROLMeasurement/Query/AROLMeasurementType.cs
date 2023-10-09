using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    public class AROLMeasurementType : ObjectType<AROLMeasurement>
    {
        // Configure method is used to define the schema for the AROLMeasurementType
        protected override void Configure(IObjectTypeDescriptor<AROLMeasurement> descriptor)
        {
            // Set a description for the AROLMeasurementType
            descriptor.Description("Represents the type of the arol measurement.");

            // Define fields for the AROLMeasurementType and set their descriptions
            descriptor
               .Field(r => r.Measurement_Id)
               .Description("Represents the id of the measurement.");

            descriptor
               .Field(r => r.X_Misreg)
               .Description("Represents the X-coordinate misregistration.");

            descriptor
               .Field(r => r.Y_Misreg)
               .Description("Represents the Y-coordinate misregistration.");

            descriptor
               .Field(r => r.X_Tis)
               .Description("Represents the X-coordinate TIS.");

            descriptor
               .Field(r => r.Y_Tis)
               .Description("Represents the Y-coordinate TIS.");

            descriptor
               .Field(r => r.Couple_Id)
               .Description("Represents the couple ID.");

            descriptor
               .Field(r => r.Run_Start_Time)
               .Description("Represents the start time of the run.");

            descriptor
               .Field(r => r.Measured_Tis)
               .Description("Indicates if TIS is measured.");

            descriptor
               .Field(r => r.X_Wis)
               .Description("Represents the X-coordinate WIS.");

            descriptor
               .Field(r => r.Y_Wis)
               .Description("Represents the Y-coordinate WIS.");

            // Define a field for measurement and set its description and resolver
            descriptor
               .Field(r => r.Measurement)
               .ResolveWith<Resolvers>(p => p.GetMeasurement(default!, default!))
               .Description("Represents the associated Measurement.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching measurement associated with a AROLMeasurement
            public async Task<Measurement> GetMeasurement([Parent] AROLMeasurement aROLmeasurement, [Service] MeasurementDataLoader dataLoader)
            {
                var measurement = await dataLoader.LoadAsync(aROLmeasurement.Measurement_Id);
                return measurement;
            }
        }
    }
}