using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Entities.Local
{
    public class ReportItem
    {
        public Guid Run_Id { get; set; }
        public Guid? Recipe_Run_Id { get; set; }
        public string LotName { get; set; }
        public string RecipeName { get; set; }
        public DateTime StartRunDate { get; set; }
        public DateTime EndRunDate { get; set; }
        public string RunTime { get; set; }
        public string LotStatus { get; set; }
        public string Reason { get; set; }
        public bool IsDisabled { get; set; }
    }
}
