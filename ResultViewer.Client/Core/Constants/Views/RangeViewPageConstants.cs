using ResultViewer.Client.Core.Entities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ResultViewer.Client.Core.Enums.RangeViewEnums;

namespace ResultViewer.Client.Core.Constants.Views
{
    public class RangeViewPageConstants
    {
        public string Url { get; set; }
        public Dictionary<RangeModifierField, RangeModifier> SelectedRanges { get; set; }
    }
}
