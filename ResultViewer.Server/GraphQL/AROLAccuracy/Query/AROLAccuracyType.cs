using ResultViewer.Server.DataLoaders;
using ResultViewer.Server.Models;
using ResultViewer.Server.Repositories.Interfaces;

namespace ResultViewer.Server.GraphQL.Query
{
    public class AROLAccuracyType : ObjectType<AROLAccuracy>
    {
        // Configure method is used to define the schema for the AROLAccuracyType
        protected override void Configure(IObjectTypeDescriptor<AROLAccuracy> descriptor)
        {
            // Set a description for the AROLAccuracyType
            descriptor.Description("Represents the type of the arol accuracy.");

            // Define fields for the AROLAccuracyType and set their descriptions
            descriptor
               .Field(r => r.Measurement_Id)
               .Description("Represents the id of the measurement.");

            descriptor
               .Field(r => r.X_Pasym_Ovl)
               .Description("Represents the X Pasym OVL value.");

            descriptor
                .Field(r => r.X_Pasym_Ovl_3S)
                .Description("Represents the X Pasym OVL 3S value.");

            descriptor
                .Field(r => r.X_Pasym_D1)
                .Description("Represents the X Pasym D1 value.");

            descriptor
                .Field(r => r.X_Pasym_D1_3S)
                .Description("Represents the X Pasym D1 3S value.");

            descriptor
                .Field(r => r.X_Pasym_D2)
                .Description("Represents the X Pasym D2 value.");

            descriptor
                .Field(r => r.X_Pasym_D2_3S)
                .Description("Represents the X Pasym D2 3S value.");

            descriptor
                .Field(r => r.X_Pplin_Dovl)
                .Description("Represents the X Pplin DOVL value.");

            descriptor
                .Field(r => r.X_Pplin_Chirsq)
                .Description("Represents the X Pplin CHIRSQ value.");

            descriptor
                .Field(r => r.X_Pplin_A_Slope)
                .Description("Represents the X Pplin A SLOPE value.");

            descriptor
                .Field(r => r.X_Pplin_A_Drsqr)
                .Description("Represents the X Pplin A DRSQR value.");

            descriptor
                .Field(r => r.X_Pupil_3S)
                .Description("Represents the X Pupil 3S value.");

            descriptor
                .Field(r => r.Run_Start_Time)
                .Description("Represents the run start time.");

            descriptor
                .Field(r => r.Y_Pasym_Ovl)
                .Description("Represents the Y Pasym OVL value.");

            descriptor
                .Field(r => r.Y_Pasym_Ovl_3S)
                .Description("Represents the Y Pasym OVL 3S value.");

            descriptor
                .Field(r => r.Y_Pasym_D1)
                .Description("Represents the Y Pasym D1 value.");

            descriptor
                .Field(r => r.Y_Pasym_D1_3S)
                .Description("Represents the Y Pasym D1 3S value.");

            descriptor
                .Field(r => r.Y_Pasym_D2)
                .Description("Represents the Y Pasym D2 value.");

            descriptor
                .Field(r => r.Y_Pasym_D2_3S)
                .Description("Represents the Y Pasym D2 3S value.");

            descriptor
                .Field(r => r.Y_Pplin_Dovl)
                .Description("Represents the Y Pplin DOVL value.");

            descriptor
                .Field(r => r.Y_Pplin_Chirsq)
                .Description("Represents the Y Pplin CHIRSQ value.");

            descriptor
                .Field(r => r.Y_Pplin_A_Slope)
                .Description("Represents the Y Pplin A SLOPE value.");

            descriptor
                .Field(r => r.Y_Pplin_A_Drsqr)
                .Description("Represents the Y Pplin A DRSQR value.");

            descriptor
                .Field(r => r.Y_Pupil_3S)
                .Description("Represents the Y Pupil 3S value.");

            descriptor
                .Field(r => r.X_Ovl_Sensitivity)
                .Description("Represents the X OVL sensitivity value.");

            descriptor
                .Field(r => r.X_Valid_Pixels)
                .Description("Represents the X valid pixels value.");

            descriptor
                .Field(r => r.Y_Ovl_Sensitivity)
                .Description("Represents the Y OVL sensitivity value.");

            descriptor
                .Field(r => r.Y_Valid_Pixels)
                .Description("Represents the Y valid pixels value.");

            descriptor
                .Field(r => r.X_Reflectivity)
                .Description("Represents the X reflectivity value.");

            descriptor
                .Field(r => r.Y_Reflectivity)
                .Description("Represents the Y reflectivity value.");

            descriptor
                .Field(r => r.X_Intensity)
                .Description("Represents the X intensity value.");

            descriptor
                .Field(r => r.Y_Intensity)
                .Description("Represents the Y intensity value.");

            descriptor
                .Field(r => r.X_Pupil_R)
                .Description("Represents the X pupil R value.");

            descriptor
                .Field(r => r.Y_Pupil_R)
                .Description("Represents the Y pupil R value.");

            descriptor
                .Field(r => r.X_MEB)
                .Description("Represents the X MEB value.");

            descriptor
                .Field(r => r.Y_MEB)
                .Description("Represents the Y MEB value.");

            descriptor
                .Field(r => r.X_PCI)
                .Description("Represents the X PCI value.");

            descriptor
                .Field(r => r.Y_PCI)
                .Description("Represents the Y PCI value.");

            descriptor
                .Field(r => r.X_Sep_Intensity_1)
                .Description("Represents the X separation intensity 1 value.");

            descriptor
                .Field(r => r.Y_Sep_Intensity_1)
                .Description("Represents the Y separation intensity 1 value.");

            descriptor
                .Field(r => r.X_Sep_Intensity_2)
                .Description("Represents the X separation intensity 2 value.");

            descriptor
                .Field(r => r.Y_Sep_Intensity_2)
                .Description("Represents the Y separation intensity 2 value.");

            descriptor
                .Field(r => r.X_Sep_Intensity_3)
                .Description("Represents the X separation intensity 3 value.");

            descriptor
                .Field(r => r.Y_Sep_Intensity_3)
                .Description("Represents the Y separation intensity 3 value.");

            descriptor
                .Field(r => r.X_Sep_Intensity_4)
                .Description("Represents the X separation intensity 4 value.");

            descriptor
                .Field(r => r.Y_Sep_Intensity_4)
                .Description("Represents the Y separation intensity 4 value.");

            descriptor
                .Field(r => r.Acq_Correction_H)
                .Description("Represents the acquisition correction H value.");

            descriptor
                .Field(r => r.Acq_Correction_V)
                .Description("Represents the acquisition correction V value.");

            descriptor
                .Field(r => r.X_Max_Signal)
                .Description("Represents the X maximum signal value.");

            descriptor
                .Field(r => r.Y_Max_Signal)
                .Description("Represents the Y maximum signal value.");

            descriptor
                .Field(r => r.X_PCA_Ratio)
                .Description("Represents the X PCA ratio value.");

            descriptor
                .Field(r => r.Y_PCA_Ratio)
                .Description("Represents the Y PCA ratio value.");

            descriptor
                .Field(r => r.X_SE_Max_WL)
                .Description("Represents the X SE maximum WL value.");

            descriptor
                .Field(r => r.Y_SE_Max_WL)
                .Description("Represents the Y SE maximum WL value.");

            descriptor
                .Field(r => r.X_SE_Mean_WL)
                .Description("Represents the X SE mean WL value.");

            descriptor
                .Field(r => r.Y_SE_Mean_WL)
                .Description("Represents the Y SE mean WL value.");

            descriptor
                .Field(r => r.X_Delta_A_Mean)
                .Description("Represents the X delta A mean value.");

            descriptor
                .Field(r => r.Y_Delta_A_Mean)
                .Description("Represents the Y delta A mean value.");

            descriptor
                .Field(r => r.X_Delta_A_3SIG)
                .Description("Represents the X delta A 3SIG value.");

            descriptor
                .Field(r => r.Y_Delta_A_3SIG)
                .Description("Represents the Y delta A 3SIG value.");

            descriptor
                .Field(r => r.X_SE_Mean_Max_WL_Ratio)
                .Description("Represents the X SE mean max WL ratio value.");

            descriptor
                .Field(r => r.Y_SE_Mean_Max_WL_Ratio)
                .Description("Represents the Y SE mean max WL ratio value.");

            descriptor
                .Field(r => r.X_Pad_To_Pad_Ratio)
                .Description("Represents the X pad-to-pad ratio value.");

            descriptor
                .Field(r => r.Y_Pad_To_Pad_Ratio)
                .Description("Represents the Y pad-to-pad ratio value.");

            descriptor
                .Field(r => r.MMA_Found_Location_Diff_H)
                .Description("Represents the MMA found location diff H value.");

            descriptor
                .Field(r => r.MMA_Found_Location_Diff_V)
                .Description("Represents the MMA found location diff V value.");

            descriptor
                .Field(r => r.X_PV_Predictor)
                .Description("Represents the X PV predictor value.");

            descriptor
                .Field(r => r.X_TIS_Predictor)
                .Description("Represents the X TIS predictor value.");

            descriptor
                .Field(r => r.Y_PV_Predictor)
                .Description("Represents the Y PV predictor value.");

            descriptor
                .Field(r => r.Y_TIS_Predictor)
                .Description("Represents the Y TIS predictor value.");

            descriptor
                .Field(r => r.Acq_Score)
                .Description("Represents the acquisition score value.");

            descriptor
                .Field(r => r.Acq_Method)
                .Description("Represents the acquisition method value.");

            descriptor
                .Field(r => r.Acq_Mode)
                .Description("Represents the acquisition mode value.");

            descriptor
                .Field(r => r.X_BPW)
                .Description("Represents the X BPW value.");

            descriptor
                .Field(r => r.Y_BPW)
                .Description("Represents the Y BPW value.");

            descriptor
                .Field(r => r.X_SPW)
                .Description("Represents the X SPW value.");

            descriptor
                .Field(r => r.Y_SPW)
                .Description("Represents the Y SPW value.");

            // Define a field for measurement and set its description and resolver
            descriptor
               .Field(r => r.Measurement)
               .ResolveWith<Resolvers>(p => p.GetMeasurement(default!, default!))
               .Description("Represents the associated Measurement.");
        }

        // This private class contains resolver methods for the fields
        private class Resolvers
        {
            // Resolver method for fetching measurement associated with a AROLAccuracy
            public async Task<Measurement> GetMeasurement([Parent] AROLAccuracy aROLAccuracy, [Service] MeasurementDataLoader dataLoader)
            {
                var measurement = await dataLoader.LoadAsync(aROLAccuracy.Measurement_Id);
                return measurement;
            }
        }
    }
}
