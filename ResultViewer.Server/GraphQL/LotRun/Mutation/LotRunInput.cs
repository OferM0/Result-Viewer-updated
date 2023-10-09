namespace ResultViewer.Server.GraphQL.Mutation
{
    public class LotRunInput
    {
        public string Lot_Name { get; set; }

        public string Recipe_Name { get; set; }

        public float Run_Start_Time { get; set; }

        public float Run_End_Time { get; set; }

        public float Lot_Status { get; set; }
    }
}
