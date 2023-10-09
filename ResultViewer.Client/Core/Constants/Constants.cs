using ResultViewer.Client.Core.Constants.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Constants
{
    // This class serves as a container for various constants and configurations used throughout the application.
    public class ConstantsBase
    {
        public SidebarComponentConstants SidebarComponent { get; set; }

        public PaginationComponentConstants PaginationComponent { get; set; }

        public ReportViewPageConstants ReportViewPage { get; set; }

        public ChartsComponentConstants ChartsComponent { get; set; }

        public LotItemComponentConstants LotItemComponent { get; set; }

        public AnalysisViewPageConstants AnalysisViewPage { get; set; }
        public RangeViewPageConstants RangeViewPage { get; set; }
    }
}

