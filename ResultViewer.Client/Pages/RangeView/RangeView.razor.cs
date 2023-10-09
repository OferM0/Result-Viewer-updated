using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResultViewer.Client.Pages.RangeView
{
    public partial class RangeView
    {
        [Inject]
        ConstantsBase Constants { get; set; } // Injected instance of ConstantsBase for configuration

        [Inject]
        RangeViewModel rangeViewModel { get; set; } // Injected instance of RangeViewModel for managing range modifiers

        private string selectedField; // Currently selected field for setting the range
        private double minValue = 0; // Minimum value of the range
        private double maxValue = 0; // Maximum value of the range

        // Get the list of selected range modifiers from the injected RangeViewModel
        private List<RangeModifier> selectedRanges => rangeViewModel.SelectedRanges;

        // Determine if the "Save" button should be disabled based on input conditions
        private bool IsSaveDisabled => string.IsNullOrEmpty(selectedField) || minValue >= maxValue;

        // Method called when the component is initialized
        protected override async Task OnInitializedAsync() { }

        // Method to save the range modifier
        private void SaveRange()
        {
            if (IsSaveDisabled)
                return;

            // Find the existing range modifier for the selected field, if it exists
            var existingRange = selectedRanges.FirstOrDefault(r => r.Field == selectedField);

            if (existingRange != null)
            { 
                existingRange.MinValue = minValue;
                existingRange.MaxValue = maxValue;
            }

            rangeViewModel.SelectedRanges = selectedRanges; // Trigger the property setter to save the changes

            ClearInputs(); // Clear input fields after saving
        }

        // Method to clear input fields
        private void ClearInputs()
        {
            selectedField = string.Empty;
            minValue = 0;
            maxValue = 0;
        }

        // Method to clear a specific field's range modifier
        private void ClearField(RangeModifier range)
        {
            var existingRange = selectedRanges.FirstOrDefault(r => r.Field == range.Field);
            if (existingRange != null)
            {
                existingRange.MinValue = null;
                existingRange.MaxValue = null;
            }

            rangeViewModel.SelectedRanges = selectedRanges; // Trigger the property setter to save the changes
        }
    }
}
