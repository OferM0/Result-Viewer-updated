using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Constants.Views
{
    public  class ChartsComponentConstants
    {
        public PieChartConstants PieChartComponent { get; set; }
        public BarChartConstants BarChartComponent { get; set; }
    }

    public class PieChartConstants
    {
        public List<string> Lables { get; set; }
    }
    public class BarChartConstants
    {
        public List<string> Lables { get; set; }
    }
}
