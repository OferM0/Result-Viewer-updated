using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.ViewModels;

namespace ResultViewer.Client.Pages.AnalysisView.Components.RecipeAnalysisItem
{
    public partial class RecipeAnalysisItem
    {
        [Inject]
        ConstantsBase Constants { get; set; } // Injected instance of ConstantsBase for configuration

        [Inject]
        RangeViewModel rangeViewModel { get; set; } // Injected instance of RangeViewModel for managing ranges

        [Parameter]
        public RecipeDataAnalysisItem item { get; set; } // Parameter representing the recipe data analysis item


        // Method to check if a value is out of the specified range
        public bool IsOutOfRange(double value, double? minValue, double? maxValue)
        {
            if (minValue == null || maxValue == null)
            {              
                return false; // No range specified, so the value is not out of range
            }

            return value < minValue || value > maxValue; // Check if the value is out of range
        }

        public bool IsAnyPropertyOutOfRange(RecipeDataAnalysisItem item)
        {
            // Check each property against its corresponding range, if available
            return IsOutOfRange(item.TIS_Mean_X, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS Mean X")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS Mean X")?.MaxValue) ||
            IsOutOfRange(item.TIS_Mean_Y, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS Mean Y")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS Mean Y")?.MaxValue) ||
            IsOutOfRange(item.TIS_3Sigma_X, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS 3σ X")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS 3σ X")?.MaxValue) ||
            IsOutOfRange(item.TIS_3Sigma_Y, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS 3σ Y")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TIS 3σ Y")?.MaxValue) ||
            IsOutOfRange(item.Precision_X, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "Prec X")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "Prec X")?.MaxValue) ||
            IsOutOfRange(item.Precision_Y, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "Prec Y")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "Prec Y")?.MaxValue) ||
            IsOutOfRange(item.TMU_X, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TMU X")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TMU X")?.MaxValue) ||
            IsOutOfRange(item.TMU_Y, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TMU Y")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TMU Y")?.MaxValue) ||
            IsOutOfRange(item.TMU_Total, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TMU Total")?.MinValue, rangeViewModel.SelectedRanges.FirstOrDefault(r => r.Field == "TMU Total")?.MaxValue);
        }
    }
}
