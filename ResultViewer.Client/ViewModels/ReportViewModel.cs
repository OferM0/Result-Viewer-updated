using ResultViewer.Client.Models;
using System.ComponentModel;

namespace ResultViewer.Client.ViewModels
{
    public interface IReportViewModel
    {
        // Properties to hold data for the report view
        List<IGetLotRuns_AllLotRuns> LotRuns { get; set; }
        IGetRecipeById_RecipeRunById RecipeRunById { get; set; }
        List<IGetMeasurementsByLotId_AllMeasurements> MeasurementsByLotId { get; set; }

        // Methods to fetch data for the report view asynchronously
        Task GetAllLotRuns(List<LotRunSortInput> sortOptions);
        Task GetRecipeRunById(Guid recipeId);
        Task GetMeasurementsByLotId(Guid lotId);

        // Event handler for property changed notification
        event PropertyChangedEventHandler PropertyChanged;
    }

    // Implement the ReportViewModel interface and inherit from BaseViewModel
    public class ReportViewModel : BaseViewModel, IReportViewModel
    {
        // Private fields to hold data for the report view
        private List<IGetLotRuns_AllLotRuns> _lotRuns;
        private IGetRecipeById_RecipeRunById _recipeRunById;
        private List<IGetMeasurementsByLotId_AllMeasurements> _measurementsByLotId;
        private IReportModel _reportModel;

        // Constructor, receives an instance of the IReportModel as a dependency
        public ReportViewModel(IReportModel reportModel)
        {
            _reportModel = reportModel;
        }

        // Property for the list of lot runs in the report view
        public List<IGetLotRuns_AllLotRuns> LotRuns
        {
            get => _lotRuns;
            set
            {
                // Call the SetValue method from BaseViewModel to set the value and notify property changes
                SetValue(ref _lotRuns, value);
            }
        }

        // Property for the recipe run in the report view
        public IGetRecipeById_RecipeRunById RecipeRunById
        {
            get => _recipeRunById;
            set
            {
                // Call the SetValue method from BaseViewModel to set the value and notify property changes
                SetValue(ref _recipeRunById, value);
            }
        }

        // Property for the list of measurements by lot ID in the report view
        public List<IGetMeasurementsByLotId_AllMeasurements> MeasurementsByLotId
        {
            get => _measurementsByLotId;
            set
            {
                // Call the SetValue method from BaseViewModel to set the value and notify property changes
                SetValue(ref _measurementsByLotId, value);
            }
        }

        // Method to fetch all lot runs asynchronously
        public async Task GetAllLotRuns(List<LotRunSortInput> sortOptions)
        {
            IsBusy = true; // Set the IsBusy flag to indicate that the operation is in progress
            LotRuns = await _reportModel.GetAllLotRuns(sortOptions); // Call the corresponding method on the report model
            OnPropertyChanged(nameof(LotRuns)); // Notify that the LotRuns property has changed
            IsBusy = false; // Set the IsBusy flag back to false to indicate that the operation is complete
        }

        // Method to fetch a recipe run by ID asynchronously
        public async Task GetRecipeRunById(Guid recipeId)
        {
            IsBusy = true; // Set the IsBusy flag to indicate that the operation is in progress
            RecipeRunById = await _reportModel.GetRecipeRunById(recipeId); // Call the corresponding method on the report model
            OnPropertyChanged(nameof(RecipeRunById)); // Notify that the RecipeRunById property has changed
            IsBusy = false; // Set the IsBusy flag back to false to indicate that the operation is complete
        }

        // Method to fetch measurements by lot ID asynchronously
        public async Task GetMeasurementsByLotId(Guid lotId)
        {
            IsBusy = true; // Set the IsBusy flag to indicate that the operation is in progress
            MeasurementsByLotId = await _reportModel.GetAllMeasurementsByLotId(lotId); // Call the corresponding method on the report model
            OnPropertyChanged(nameof(MeasurementsByLotId)); // Notify that the MeasurementsByLotId property has changed
            IsBusy = false; // Set the IsBusy flag back to false to indicate that the operation is complete
        }
    }
}
