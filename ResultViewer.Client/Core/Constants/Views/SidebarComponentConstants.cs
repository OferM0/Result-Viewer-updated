using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Constants.Views
{
    public class SidebarComponentConstants
    {
        public string MainHeaderTitle { get; set; }
        public string DialogsTitle { get; set; }
        public string ViewsTitle { get; set; }
        public DialogsConstants Dialogs { get; set; }
        public ViewsConstants Views { get; set; }
    }

    public class DialogsConstants
    {
        public string Open { get; set; }
        public string Save { get; set; }
        public string Print { get; set; }
        public string Copy { get; set; }
        public string Delete { get; set; }
    }

    public class ViewsConstants
    {
        public string Import { get; set; }
        public string Export { get; set; }
        public string Report { get; set; }
        public string CombinedLot { get; set; }
        public string ReportConfiguration { get; set; }
        public string Analysis { get; set; }
        public string Machine { get; set; }
        public string Settings { get; set; }
    }
}
