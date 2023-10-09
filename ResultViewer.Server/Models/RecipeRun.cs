using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResultViewer.Server.Models
{
    [Table("RECIPE_RUN")]
    public class RecipeRun
    {
        [Key]
        public Guid Recipe_Run_Id { get; set; }

        [Required]
        public string Recipe_Name { get; set; }

        public decimal Recipe_Modification_Date { get; set; }

        public string Version_String { get; set; }

        public decimal Recipe_Creation_Date { get; set; }

        public double Export_To { get; set; }

        public double Eye_Point_Position_X { get; set; }

        public double Eye_Point_Position_Y { get; set; }

        public decimal I_Die_O { get; set; }

        public decimal J_Die_O { get; set; }

        public decimal Wafer_Size { get; set; }

        public decimal Wafer_Orientation { get; set; }

        public double Die_Size_X { get; set; }

        public double Die_Size_Y { get; set; }

        public double Chip_Die_X { get; set; }

        public double Chip_Die_Y { get; set; }

        public decimal Wafer_Type { get; set; }

        public decimal Shape { get; set; }

        public decimal Bricked { get; set; }

        public double X_WM_Offset_Vector { get; set; }

        public double Y_WM_Offset_Vector { get; set; }

        public decimal Wafer_Map_Real_Size_X { get; set; }

        public decimal Wafer_Map_Real_Size_Y { get; set; }

        public decimal Start_Pos_Wafer_X { get; set; }

        public decimal Start_Pos_Wafer_Y { get; set; }

        public byte[] Wafer_Die_Array { get; set; }

        public decimal R1_Origin_Point { get; set; }

        public double EP_TO_LLC_X { get; set; }

        public double EP_TO_LLC_Y { get; set; }

        public Guid Original_Recipe_Run_Id { get; set; }

        public decimal Wafer_Load_Angle { get; set; }

        public ICollection<LotRun> LotRuns { get;set; } = new List<LotRun>();
        public ICollection<TestRecipeRun> TestRecipeRuns { get;set; } = new List<TestRecipeRun>();
    }
}
