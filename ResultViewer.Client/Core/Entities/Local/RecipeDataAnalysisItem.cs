using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Entities.Local
{
    public class RecipeDataAnalysisItem
    {
        public DateTime RunDate { get; set; }
        public string LotName { get; set; }
        public double TIS_Mean_X { get; set; } = 0;
        public double TIS_Mean_Y { get; set; } = 0;
        public double TIS_3Sigma_X { get; set; } = 0;
        public double TIS_3Sigma_Y { get; set; } = 0;
        public double Precision_X { get; set; } = 0;
        public double Precision_Y { get; set; } = 0;
        public double TMU_X { get; set; } = 0;
        public double TMU_Y { get; set; } = 0;
        public double TMU_Total { get; set; } = 0;

    }
}
