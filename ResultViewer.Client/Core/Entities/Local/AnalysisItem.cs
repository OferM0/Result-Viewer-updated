using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Entities.Local
{
    public class AnalysisItem
    {
        public Guid Recipe_Run_Id { get; set; }
        public string RecipeName { get; set; }
        public DateTime FirstRunDate { get; set; }
        public DateTime LastRunDate { get; set; }
        public int Count { get; set; }
    }
}
