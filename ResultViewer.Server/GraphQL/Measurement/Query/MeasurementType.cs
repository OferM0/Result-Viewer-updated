using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Helpers;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    public class MeasurementType : ObjectType<Measurement>
    {
        // Configure method is used to define the schema for the MeasurementType
        protected override void Configure(IObjectTypeDescriptor<Measurement> descriptor)
        {
            // Set a description for the MeasurementType
            descriptor.Description("Represents the type of the measurement.");

            // Define fields for the MeasurementType and set their descriptions
            descriptor
               .Field(r => r.Measurement_Id)
               .Description("Represents the id of the measurement.");

            descriptor
               .Field(r => r.Run_Id)
               .Description("Represents the id of the lot run.");

            descriptor
               .Field(r => r.Wafer_Ordinal_Num)
               .Description("Represents the ordinal number of the wafer.");

            descriptor
               .Field(r => r.Wafer_Id)
               .Description("Represents the id of the wafer.");

            descriptor
               .Field(r => r.Slot_Num)
               .Description("Represents the slot number.");

            descriptor
               .Field(r => r.Test_Num)
               .Description("Represents the test number.");

            descriptor
               .Field(r => r.X_Die)
               .Description("Represents the X-coordinate of the die.");

            descriptor
               .Field(r => r.Y_Die)
               .Description("Represents the Y-coordinate of the die.");

            descriptor
               .Field(r => r.X_Element)
               .Description("Represents the X-coordinate of the element.");

            descriptor
               .Field(r => r.Y_Element)
               .Description("Represents the Y-coordinate of the element.");

            descriptor
               .Field(r => r.X_Location)
               .Description("Represents the X-coordinate of the location.");

            descriptor
               .Field(r => r.Y_Location)
               .Description("Represents the Y-coordinate of the location.");

            descriptor
               .Field(r => r.Static_Iteration)
               .Description("Represents the static iteration.");

            descriptor
                .Field(r => r.Orientation)
                .Description("Represents the orientation.");

            descriptor
                .Field(r => r.Slice)
                .Description("Represents the slice.");

            descriptor
                .Field(r => r.Status)
                .Description("Represents the status.");

            // Error message - we need to try receiving it as a string, so we added a resolver.
            descriptor
                .Field(r => r.Fail_Reason)
                .ResolveWith<Resolvers>(p => p.HandleErrorMessage(default!, default!))
                .Description("Represents the reason for failure.");

            descriptor
                .Field(r => r.Site_Serial_Num)
                .Description("Represents the site serial number.");

            descriptor
                .Field(r => r.Image_Path1)
                .Description("Represents the path of image 1.");

            descriptor
                .Field(r => r.Image_Path2)
                .Description("Represents the path of image 2.");

            descriptor
                .Field(r => r.Image_Path3)
                .Description("Represents the path of image 3.");

            descriptor
                .Field(r => r.Test_Label)
                .Description("Represents the test label.");

            descriptor
                .Field(r => r.Wafer_X)
                .Description("Represents the X-coordinate of the wafer.");

            descriptor
                .Field(r => r.Wafer_Y)
                .Description("Represents the Y-coordinate of the wafer.");

            descriptor
                .Field(r => r.Is_Unpatterned)
                .Description("Indicates if the measurement is unpatterned.");

            descriptor
                .Field(r => r.Is_Vertical_Target)
                .Description("Indicates if the measurement is a vertical target.");

            descriptor
                .Field(r => r.Run_Start_Time)
                .Description("Represents the start time of the run.");

            descriptor
                .Field(r => r.Archive_Path)
                .Description("Represents the archive path.");

            descriptor
                .Field(r => r.Is_Compressed)
                .Description("Indicates if the measurement is compressed.");

            descriptor
                .Field(r => r.Scheme_Libraries)
                .Description("Represents the scheme libraries.");

            // Define a field for LotRun and set its description and resolver
            descriptor
                .Field(p => p.LotRun)
                .ResolveWith<Resolvers>(p => p.GetLotRun(default!, default!))
                .Description("Represents a list of available lot runs for this measurement.");

            // Define a field for AROLMeasurement and set its description and resolver
            descriptor
                .Field(p => p.AROLMeasurement)
                .ResolveWith<Resolvers>(p => p.GetAROLMeasurement(default!, default!))
                .Description("Represents an object of available AROLMeasurement for this measurement.");

            // Define a field for AROLAccuracy and set its description and resolver
            descriptor
                .Field(p => p.AROLAccuracy)
                .ResolveWith<Resolvers>(p => p.GetAROLAccuracy(default!, default!))
                .Description("Represents an object of available AROLAccuracy for this measurement.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching lotRun associated with a Measurement
            public async Task<LotRun> GetLotRun([Parent] Measurement measurement, [Service] LotRunDataLoader dataLoader)
            {
                var lotRun = await dataLoader.LoadAsync(measurement.Run_Id);
                return lotRun;
            }

            // Resolver method for fetching AROLMeasurement associated with a Measurement
            public async Task<AROLMeasurement> GetAROLMeasurement([Parent] Measurement measurement, [Service] AROLMeasurementDataLoader dataLoader)
            {
                var AROLMeasurement = await dataLoader.LoadAsync(measurement.Measurement_Id);
                return AROLMeasurement;
            }

            // Resolver method for fetching GetAROLAccuracy associated with a Measurement
            public async Task<AROLAccuracy> GetAROLAccuracy([Parent] Measurement measurement, [Service] AROLAccuracyDataLoader dataLoader)
            {
                var aROLAccuracy = await dataLoader.LoadAsync(measurement.Measurement_Id);
                return aROLAccuracy;
            }

            // Resolver method for send the error message associated with the number
            public string HandleErrorMessage([Parent] Measurement measurement, [Service] ErrorMessageHelper helper)
            {
                var errorMessage = helper.ErrorMessages[(int)measurement.Fail_Reason];
                return errorMessage;
            }
        }
    }
}
