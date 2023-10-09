using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Entities.Local
{
    public class RawDataItem
    {
        public string TestNum { get; set; }
        public string TestType { get; set; }
        public string FieldX { get; set; }
        public string FieldY { get; set; }
        public string ArrayX { get; set; }
        public string ArrayY { get; set; }
        public string LinkGratingXImages { get; set; }
        public string LinkGratingYImages { get; set; }
        public string MisregX { get; set; }
        public string MisregY { get; set; }
        public string TISXCorrection { get; set; }
        public string TISYCorrection { get; set; }
        public string PassOrFail { get; set; }
        public string FailReason { get; set; }
        public string PasymOvlX { get; set; }
        public string PasymOvlY { get; set; }
    }
}
