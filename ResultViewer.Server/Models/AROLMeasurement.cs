using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResultViewer.Server.Models
{
    [Table("AROL_MEASUREMENT")]
    public class AROLMeasurement
    {
        [Key]
        public Guid Measurement_Id { get; set; }
        public double? X_Misreg { get; set; }
        public double? Y_Misreg { get; set; }
        public double? X_Tis { get; set; }
        public double? Y_Tis { get; set; }
        public decimal? Couple_Id { get; set; }
        public double Run_Start_Time { get; set; }
        public byte Measured_Tis { get; set; }
        public double? X_Wis { get; set; }
        public double? Y_Wis { get; set; }
        public Measurement Measurement { get; set; }
    }
}
