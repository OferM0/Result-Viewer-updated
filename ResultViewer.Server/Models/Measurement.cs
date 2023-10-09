using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ResultViewer.Server.Models
{
    [Table("MEASUREMENT")]
    public class Measurement
    {
        [Key]
        public Guid Measurement_Id { get; set; }
        public double Measurement_Type { get; set; }
        public Guid Run_Id { get; set; }
        public double Wafer_Ordinal_Num { get; set; }
        public string Wafer_Id { get; set; }
        public double Slot_Num { get; set; }
        public double Test_Num { get; set; }
        public double X_Die { get; set; }
        public double Y_Die { get; set; }
        public double X_Element { get; set; }
        public double Y_Element { get; set; }
        public double X_Location { get; set; }
        public double Y_Location { get; set; }
        public double Static_Iteration { get; set; }
        public double Orientation { get; set; }
        public double Slice { get; set; }
        public double Status { get; set; }
        public double Fail_Reason { get; set; }
        public double Site_Serial_Num { get; set; }
        public string Image_Path1 { get; set; }
        public string Image_Path2 { get; set; }
        public string Image_Path3 { get; set; }
        public string Test_Label { get; set; }
        public double Wafer_X { get; set; }
        public double Wafer_Y { get; set; }
        public decimal Is_Unpatterned { get; set; }
        public decimal Is_Vertical_Target { get; set; }
        public double Run_Start_Time { get; set; }
        public string Archive_Path { get; set; }
        public Byte Is_Compressed { get; set; }
        public decimal Scheme_Libraries { get; set; }
        public LotRun LotRun { get; set; } 
        public AROLMeasurement AROLMeasurement { get; set; }
        public AROLAccuracy AROLAccuracy { get; set; }
    }
}
