using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResultViewer.Server.Models
{
    [Table("LOT_RUN")]
    public class LotRun
    {
        [Key]
        public Guid Run_Id { get; set; }

        public string Lot_Name { get; set; }

        public double Lot_Status { get; set; }

        public string Recipe_Name { get; set; }

        public double Run_Start_Time { get; set; }

        public double Run_End_Time { get; set; }

        public string Stepper_Id { get; set; }

        public string Operator_Name { get; set; }

        public string Tool_Id { get; set; }

        public double Failed_Wafers { get; set; }

        public double Failed_Sites { get; set; }

        public string Comments { get; set; }

        public string Stepper_Id_2 { get; set; }

        public Guid? Recipe_Run_Id { get; set; }

        public double Slot_Map { get; set; }

        public double Iteration { get; set; }

        public double Calibration_Mode { get; set; }

        public double Port_Num { get; set; }

        public double ARP_Flag { get; set; }

        public double TIS_Mode { get; set; }

        public double Access_Method { get; set; }

        public decimal Lied_File_Flag { get; set; }

        public string Control_Job_Id { get; set; }

        public string Process_Job_Id { get; set; }

        public string Carrier_Id { get; set; }

        public string Analysis_Recipe_Name { get; set; }

        public decimal Handling_Mode { get; set; }

        public decimal DS_Iteration_Number { get; set; }

        public Byte Calc_Qmerit { get; set; }

        public Byte Is_ATM_Selected { get; set; }

        public Byte Calc_Qmerit_Layer { get; set; }

        public Byte Modeled_TIS_Run_Enabled { get; set; }

        public Int64 Imaging_Accuracy_Metrics { get; set; }

        public ICollection<WaferRun> WaferRuns { get; set; } = new List<WaferRun>();

        public RecipeRun RecipeRun { get; set; }
        public ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    }
}
