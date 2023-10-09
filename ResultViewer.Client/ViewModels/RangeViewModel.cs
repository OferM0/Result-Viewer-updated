using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.Models;
using System.ComponentModel;
using static ResultViewer.Client.Core.Enums.RangeViewEnums;

namespace ResultViewer.Client.ViewModels
{
    public class RangeViewModel : BaseViewModel
    {
        private bool isDataLoaded = false; // Flag to track whether data is loaded from storage

        private List<RangeModifier> selectedRanges = new List<RangeModifier>(); // List to store selected ranges

        // Constructor that takes ConstantsBase as a parameter
        public RangeViewModel(ConstantsBase Constants)
        {
            // Define the order of fields for range modifiers
            var fieldOrder = new List<RangeModifierField>
            {
                RangeModifierField.TIS_Mean_X,
                RangeModifierField.TIS_Mean_Y,
                RangeModifierField.TIS_3σ_X,
                RangeModifierField.TIS_3σ_Y,
                RangeModifierField.Prec_X,
                RangeModifierField.Prec_Y,
                RangeModifierField.TMU_X,
                RangeModifierField.TMU_Y,
                RangeModifierField.TMU_Total
            };

            // Initialize selectedRanges by filtering and mapping values from Constants
            selectedRanges = fieldOrder
            .Where(field => Constants.RangeViewPage.SelectedRanges.ContainsKey(field))
            .Select(field => Constants.RangeViewPage.SelectedRanges[field])
            .ToList();
        }

        public List<RangeModifier> SelectedRanges
        {
            get
            {
                if (!isDataLoaded) // Load from storage only the first time the getter is used
                {
                    // Retrieve the serialized list from storage
                    string serializedList = GetDataStorageValue("SelectedRanges", string.Empty);

                    if (!string.IsNullOrEmpty(serializedList))
                    {
                        // Deserialize the list from JSON
                        selectedRanges = JsonConvert.DeserializeObject<List<RangeModifier>>(serializedList);
                    }

                    isDataLoaded = true;
                }


                return selectedRanges;
            }
            set
            {
                SetValue(ref selectedRanges, value);

                // Serialize the list to JSON
                string serializedList = JsonConvert.SerializeObject(selectedRanges);

                // Save the serialized list in storage each time the setter is used
                SetDataStorageValue("SelectedRanges", serializedList);
            }
        }
    }  
}