using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResultViewer.Server.Models
{
    [Table("WAFER_RUN")]

    public class WaferRun
    {
        [Key]
        public Guid Wafer_Run_Id { get; set; }
        [Required]
        public Guid Run_Id { get; set; }
        [Required]
        public decimal Wafer_Ordinal_Num { get; set; }
        [Required]
        public decimal Wafer_Start_Time { get; set; }
        [Required]
        public decimal Wafer_End_Time { get; set; }
        [Required]
        public Byte Conditional_Site_Mode{ get; set; }
        public string Images_Meta_Data_Path { get; set; }
        public LotRun LotRun { get; set; } = new LotRun();
    }
}
