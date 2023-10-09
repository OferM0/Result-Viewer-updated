using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Enums
{
    public enum ReportViewDropDownOptions
    {
        LotSummary,
        RawData,
        FieldStatistics,
        LotStatistics,
        SiteStatistics,
        TestStatistics,
        WaferStatistics,
        TISSummary,
        ErrorSummary,
        OptimizerSummary
    }

    public enum LotSummaryTitles
    {
        Lot_ID,
        Recipe_Name,
        Tool_ID,
        Operator_ID,
        Stepper_ID,
        Cassette_ID,
        Comments,
        TIS_Mode,
        Calibration_Mode,
        Report_Date,
        Lot_Start_Date,
        Lot_End_Date,
        Total_Run_Time,
        Number_of_Measured_Sites,
        Number_of_Wafers,
        Number_of_Iterations,
        Number_of_Tests,
        Failure_Percentage,
        Recipe_Version,
        Recipe_Created,
        Recipe_Modified,
        Wafer_Size,
        Wafer_Type,
        Wafer_Orientation,
        Field_Size,
        Number_of_Dies
    }

    public enum ExportLotTitles
    {
        DateTime,
        Lot,
        Operator,
        Program,
        SlotNum,
        WaferNum,
        WaferID,
        XDieSize,
        YDieSize,
        TestID,
        TestType,
        TestLabel,
        LocationX,
        LocationY,
        FieldX,
        FieldY,
        ArrayX,
        ArrayY,
        TargetLocationX,
        TargetLocationY,
        WaferLocationX,
        WaferLocationY,
        MregX,
        MregY
    }
}
