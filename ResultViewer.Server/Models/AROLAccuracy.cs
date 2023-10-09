using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResultViewer.Server.Models
{
    [Table("AROL_ACCURACY")]
    public class AROLAccuracy
    {
        [Key]
        public Guid Measurement_Id { get; set; }
        public double? X_Pasym_Ovl { get; set; }
        public double? X_Pasym_Ovl_3S { get; set; }
        public double? X_Pasym_D1 { get; set; }
        public double? X_Pasym_D1_3S { get; set; }
        public double? X_Pasym_D2 { get; set; }
        public double? X_Pasym_D2_3S { get; set; }
        public double? X_Pplin_Dovl { get; set; }
        public double? X_Pplin_Chirsq { get; set; }
        public double? X_Pplin_A_Slope { get; set; }
        public double? X_Pplin_A_Drsqr { get; set; }
        public double? X_Pupil_3S { get; set; }
        public double? Y_Pasym_Ovl { get; set; }
        public double? Y_Pasym_Ovl_3S { get; set; }
        public double? Y_Pasym_D1 { get; set; }
        public double? Y_Pasym_D1_3S { get; set; }
        public double? Y_Pasym_D2 { get; set; }
        public double? Y_Pasym_D2_3S { get; set; }
        public double? Y_Pplin_Dovl { get; set; }
        public double? Y_Pplin_Chirsq { get; set; }
        public double? Y_Pplin_A_Slope { get; set; }
        public double? Y_Pplin_A_Drsqr { get; set; }
        public double? Y_Pupil_3S { get; set; }
        public double? X_Ovl_Sensitivity { get; set; }
        public double? X_Valid_Pixels { get; set; }
        public double? Y_Ovl_Sensitivity { get; set; }
        public double? Y_Valid_Pixels { get; set; }
        public double Run_Start_Time { get; set; }
        public double? X_Reflectivity { get; set; }
        public double? Y_Reflectivity { get; set; }
        public double? X_Intensity { get; set; }
        public double? Y_Intensity { get; set; }
        public double? X_Pupil_R { get; set; }
        public double? Y_Pupil_R { get; set; }
        public double? X_MEB { get; set; }
        public double? Y_MEB { get; set; }
        public double? X_PCI { get; set; }
        public double? Y_PCI { get; set; }
        public double? X_Sep_Intensity_1 { get; set; }
        public double? Y_Sep_Intensity_1 { get; set; }
        public double? X_Sep_Intensity_2 { get; set; }
        public double? Y_Sep_Intensity_2 { get; set; }
        public double? X_Sep_Intensity_3 { get; set; }
        public double? Y_Sep_Intensity_3 { get; set; }
        public double? X_Sep_Intensity_4 { get; set; }
        public double? Y_Sep_Intensity_4 { get; set; }
        public double? Acq_Correction_H { get; set; }
        public double? Acq_Correction_V { get; set; }
        public double? X_Max_Signal { get; set; }
        public double? Y_Max_Signal { get; set; }
        public double? X_PCA_Ratio { get; set; }
        public double? Y_PCA_Ratio { get; set; }
        public double? X_SE_Max_WL { get; set; }
        public double? Y_SE_Max_WL { get; set; }
        public double? X_SE_Mean_WL { get; set; }
        public double? Y_SE_Mean_WL { get; set; }
        public double? X_Delta_A_Mean { get; set; }
        public double? Y_Delta_A_Mean { get; set; }
        public double? X_Delta_A_3SIG { get; set; }
        public double? Y_Delta_A_3SIG { get; set; }
        public double? X_SE_Mean_Max_WL_Ratio { get; set; }
        public double? Y_SE_Mean_Max_WL_Ratio { get; set; }
        public double? X_Pad_To_Pad_Ratio { get; set; }
        public double? Y_Pad_To_Pad_Ratio { get; set; }
        public double? MMA_Found_Location_Diff_H { get; set; }
        public double? MMA_Found_Location_Diff_V { get; set; }
        public double? X_PV_Predictor { get; set; }
        public double? X_TIS_Predictor { get; set; }
        public double? Y_PV_Predictor { get; set; }
        public double? Y_TIS_Predictor { get; set; }
        public double? Acq_Score { get; set; }
        public double? Acq_Method { get; set; }
        public double? Acq_Mode { get; set; }
        public double? X_BPW { get; set; }
        public double? Y_BPW { get; set; }
        public double? X_SPW { get; set; }
        public double? Y_SPW { get; set; }
        public Measurement Measurement { get; set; }
    }
}
