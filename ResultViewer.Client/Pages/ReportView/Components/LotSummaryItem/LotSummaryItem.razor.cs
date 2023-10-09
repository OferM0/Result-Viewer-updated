using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Entities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Pages.ReportView.Components.LotSummaryItem
{
    public partial class LotSummaryItem
    {
        [Parameter]
        public SummaryItem item { get; set; }
    }
}
