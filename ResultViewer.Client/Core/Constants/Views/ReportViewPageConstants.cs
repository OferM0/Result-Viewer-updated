using ResultViewer.Client.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Constants.Views
{
    public class ReportViewPageConstants
    {
        public string Url { get; set; }
        public string StatusPassed { get; set; }
        public string StatusFailed { get; set; }
        public List<string> MainTableHeaders { get; set; }
        public SortingDropDownOptions SortingDropDownOptions { get; set; }
        public Dictionary<string, ReportViewDropDownOptions> NavigationDropDownOptions { get; set; }
        public Dictionary<string, ExportLotTitles> ExportDict { get; set; }
        public List<string> ExportTitles { get; set; }
        public LotSummary LotSummary { get; set; }
        public RawData RawData { get; set; }
    }

    public class SortingDropDownOptions
    {
        public string SortBy { get; set; }
        public string ByRecent { get; set; }
        public string ByName { get; set; }
        public string ByStatus { get; set; }
        public string ByCount { get; set; }
    }

    public class LotSummary
    {
        public List<string> MainHeaders { get; set; }
        public Dictionary<LotSummaryTitles, string> SummaryTitles { get; set; }
    }

    public class RawData
    {
        public List<string> MainHeaders { get; set; }
        public List<string> RawDataTitles { get; set; }
    }
}
