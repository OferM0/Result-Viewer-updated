using ResultViewer.Client.Core.Enums;

namespace ResultViewer.Client.Core.Constants.Views
{
    public class AnalysisViewPageConstants
    {
        public string Url { get; set; }
        public List<string> MainTableHeaders { get; set; }
        public List<string> SecondTableHeaders { get; set; }
        public string ExportTMUButtonName { get; set; }
        public string DatePickerText { get; set; }
        public Dictionary<string, ExportRecipeAnalysisTitles> ExportDict { get; set; }
    }
}
