using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ResultViewer.Server.Models
{
    [Table("TEST_RECIPE_RUN")]
    public class TestRecipeRun
    {
        [Key]
        public Guid Test_Recipe_Run_Id { get; set; }
        [Required]
        public Guid Recipe_Run_Id { get; set; }
        public decimal Test_Number { get; set; }
        public decimal Test_Type { get; set; }
        public decimal Persents_Sites_To_Fail { get; set; }
        public decimal Tis_Method { get; set; }
        public decimal Is_Arrayed { get; set; }
        public decimal Array_Dimension_X { get; set; }
        public decimal Array_Dimension_Y { get; set; }
        public double Array_Delta_X { get; set; }
        public double Array_Delta_Y { get; set; }
        public double Location_X { get; set; }
        public double Location_Y { get; set; }
        public double Site_Offset_Xy_X { get; set; }
        public double Site_Offset_Xy_Y { get; set; }
        public decimal Kla_Ref_Sell_X { get; set; }
        public decimal Kla_Ref_Sell_Y { get; set; }
        public decimal User_Ref_Sell_X { get; set; }
        public decimal User_Ref_Sell_Y { get; set; }
        public decimal Is_Enable { get; set; }
        public byte[]? Array_Test_List { get; set; }
        public string? Test_Label { get; set; }
        public decimal R3_Origin_Point { get; set; }
        public decimal Is_Trained { get; set; }
        public RecipeRun RecipeRun { get; set; } = new RecipeRun();
    }
}
