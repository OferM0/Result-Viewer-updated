using ResultViewer.Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.ViewModels
{
    public interface IAnalysisViewModel
    {
        // Properties to hold data for the analysis view
        IGetAnalysisDataByRecipeId_RecipeRunById AnalysisDataByRecipeId { get; set; }

        // Methods to fetch data for the analysis view asynchronously
        Task GetAnalysisDataByRecipeId(Guid recipeId);

        // Event handler for property changed notification
        event PropertyChangedEventHandler PropertyChanged;
    }

    // Implement the AnalysisViewModel interface and inherit from BaseViewModel
    public class AnalysisViewModel : BaseViewModel, IAnalysisViewModel
    {
        // Private fields to hold data for the analysis view
        private IGetAnalysisDataByRecipeId_RecipeRunById _analysisDataByRecipeId;
        private IAnalysisModel _analysisModel;

        // Constructor, receives an instance of the IAnalysisModel as a dependency
        public AnalysisViewModel(IAnalysisModel analysisModel)
        {
            _analysisModel = analysisModel;
        }

        // Property for the analysis data in the report view
        public IGetAnalysisDataByRecipeId_RecipeRunById AnalysisDataByRecipeId
        {
            get => _analysisDataByRecipeId;
            set
            {
                // Call the SetValue method from BaseViewModel to set the value and notify property changes
                SetValue(ref _analysisDataByRecipeId, value);
            }
        }

        // Method to fetch the analysis data by recipe ID asynchronously
        public async Task GetAnalysisDataByRecipeId(Guid recipeId)
        {
            IsBusy = true; // Set the IsBusy flag to indicate that the operation is in progress
            AnalysisDataByRecipeId = await _analysisModel.GetAnalysisDataByRecipeId(recipeId); // Call the corresponding method on the analysis model
            OnPropertyChanged(nameof(AnalysisDataByRecipeId)); // Notify that the RecipeRunById property has changed
            IsBusy = false; // Set the IsBusy flag back to false to indicate that the operation is complete
        }
    }
}
