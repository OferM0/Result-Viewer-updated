using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using ResultViewer.Client.Core.Entities.Local;
using Newtonsoft.Json;
using ResultViewer.Client.ViewModels;
using ResultViewer.Client.Core.Constants;
using System.ComponentModel;
using System.Text;
using CommunityToolkit.Maui.Storage;
using ResultViewer.Client.Core.Enums;

namespace ResultViewer.Client.Pages.AnalysisView
{
    public partial class RecipeAnalysis : IDisposable
    {
        [Inject]
        ConstantsBase Constants { get; set; } // Injected ConstantsBase for constants

        [Inject]
        IAnalysisViewModel AnalysisViewModel { get; set; } // Injected AnalysisViewModel for managing analysis-related data.

        [Inject]
        RangeViewModel rangeViewModel { get; set; } // Injected RangeViewModel for managing range-related data.

        [Inject]
        IFileSaver FileSaver { get; set; } // Injected IFileSaver for handling file saving operations.

        // The name of the recipe associated with this component.
        [Parameter] public string RecipeName { get; set; }

        // Date range information for filtering data.
        public DateRangeInfo DateRangeInfo { get; set; }

        // Lists to store recipe data analysis items.
        public List<RecipeDataAnalysisItem> DataItems { get; set; } = new List<RecipeDataAnalysisItem>();
        public List<RecipeDataAnalysisItem> FilteredDataItems { get; set; } = new List<RecipeDataAnalysisItem>();

        // List of headers for the main table in the component.
        public List<string> MainTableHeaders;

        // List of breadcrumb items for navigation.
        public List<BreadcrumbItem> Breadcrumbs { get; set; }

        // Flag indicating whether there are lots in selected range - filter
        public bool NoLotsInSelectedRangeDate { get; set; } = false;

        // Flag indicating whether filtering is applied.
        private bool IsFilter = false;

        protected override async Task OnInitializedAsync()
        {
            // Initialize table headers
            MainTableHeaders = Constants.AnalysisViewPage.SecondTableHeaders;

            // Parse query parameters from the URL.
            Dictionary<string, StringValues> querystring = QueryHelpers.ParseQuery(NavManager.ToAbsoluteUri(NavManager.Uri).Query);

            if (querystring.TryGetValue("BreadCrumbs", out var breadCrumbsInJson))
            {
                // Deserialize breadcrumb items from JSON
                Breadcrumbs = JsonConvert.DeserializeObject<List<BreadcrumbItem>>(breadCrumbsInJson);
            }

            if (querystring.TryGetValue("dateRangeInfo", out var dateRangeInfoJson))
            {
                try
                {
                    //Deserialize the JSON string back into a DateRangeInfo object
                    DateRangeInfo = JsonConvert.DeserializeObject<DateRangeInfo>(dateRangeInfoJson);

                    // Register an event handler for property changes in AnalysisViewModel.
                    AnalysisViewModel.PropertyChanged += OnPropertyChangedHandler;

                    await LoadAnalysisData();

                    await base.OnInitializedAsync();                                 
                }
                catch (JsonException)
                {
                    // Handle JSON deserialization error
                    // ...
                }
            }
        }

        private async Task LoadAnalysisData()
        {
            // Get the analysis data associated with the selected Recipe Run ID.
            await GetAnalysisDataOfRecipe(DateRangeInfo.Item.Recipe_Run_Id);

            var lotsInDateRange = AnalysisViewModel.AnalysisDataByRecipeId.LotRuns
                    .Where(lotRun =>
                    {
                        // Convert the double dates to DateTimeOffset
                        DateTimeOffset lotStartTime = DateTimeOffset.FromUnixTimeSeconds((long)lotRun.Run_Start_Time);
                        DateTimeOffset lotEndTime = DateTimeOffset.FromUnixTimeSeconds((long)lotRun.Run_End_Time);

                        // Check if the lot run is within the specified date range
                        return lotStartTime.DateTime >= DateRangeInfo.StartDate && lotEndTime.DateTime <= DateRangeInfo.EndDate;
                    })
                    .ToList();

            // Check if there are no lots in the selected date range.
            if (lotsInDateRange.Count == 0)
            {
                NoLotsInSelectedRangeDate = true;
            }

            // Separate lots into three categories based on ArolMeasurement availability.
            var lotsWithNotNullArol = lotsInDateRange
            .Where(lotRun => lotRun.Measurements.All(measurement => measurement.ArolMeasurement != null))
            .ToList();

            var lotsWithNullArol = lotsInDateRange
            .Where(lotRun => lotRun.Measurements.All(measurement => measurement.ArolMeasurement == null))
            .ToList();

            var lotsWithNullAndNotNullArol = lotsInDateRange
            .Where(lotRun => lotRun.Measurements.Any(measurement => measurement.ArolMeasurement != null)
             && lotRun.Measurements.Any(measurement => measurement.ArolMeasurement == null))
            .ToList();

            // Combine lots with null Arol measurements and lots with both null and not null Arol measurements.
            lotsWithNotNullArol.AddRange(lotsWithNullAndNotNullArol);

            // Iterate and create RecipeDataAnalysisItem for lots with null Arol measurements and add them to DataItems.                 
            foreach (var lot in lotsWithNullArol)
            {
                RecipeDataAnalysisItem recipeAnalysisItem = new RecipeDataAnalysisItem();

                recipeAnalysisItem.RunDate = DateTimeOffset.FromUnixTimeSeconds((long)lot.Run_Start_Time).DateTime;

                recipeAnalysisItem.LotName = lot.Lot_Name;

                // all numeric analysis value of recipeAnalysisItem will stay 0 as deafault
                // we dont calculate because there is no way to calculate when arol measurement is null

                DataItems.Add(recipeAnalysisItem);
            }

            foreach (var lotArol in lotsWithNotNullArol)
            {
                RecipeDataAnalysisItem recipeAnalysisItem = new RecipeDataAnalysisItem();

                recipeAnalysisItem.RunDate = DateTimeOffset.FromUnixTimeSeconds((long)lotArol.Run_Start_Time).DateTime;

                recipeAnalysisItem.LotName = lotArol.Lot_Name;

                // for x_tis mean
                double averageX_tis = lotArol.Measurements
                .Where(measurement => measurement.ArolMeasurement != null)
                .Average(measurement => measurement.ArolMeasurement.X_Tis ?? 0);

                recipeAnalysisItem.TIS_Mean_X = averageX_tis;

                //for y_tis mean
                double averageY_tis = lotArol.Measurements
                .Where(measurement => measurement.ArolMeasurement != null)
                .Average(measurement => measurement.ArolMeasurement.Y_Tis ?? 0);

                recipeAnalysisItem.TIS_Mean_Y = averageY_tis;

                //for x_tis 3 sigma
                double[] x_tisValues = lotsWithNotNullArol.SelectMany(lotArol => lotArol.Measurements)
                .Where(measurement => measurement.ArolMeasurement != null)
                .Select(measurement => measurement.ArolMeasurement.X_Tis ?? 0)
                .ToArray();

                double sumSquaredDeviationsX = x_tisValues
                .Select(x_tis => Math.Pow(x_tis - averageX_tis, 2))
                .Sum();

                double SigmaX_tis = Math.Sqrt(sumSquaredDeviationsX / x_tisValues.Length);

                double threeSigmaX_tis = 3 * SigmaX_tis;

                recipeAnalysisItem.TIS_3Sigma_X = threeSigmaX_tis;

                //for y_tis 3 sigma
                double[] y_tisValues = lotsWithNotNullArol.SelectMany(lotArol => lotArol.Measurements)
                .Where(measurement => measurement.ArolMeasurement != null)
                .Select(measurement => measurement.ArolMeasurement.Y_Tis ?? 0)
                .ToArray();

                double sumSquaredDeviationsY = y_tisValues
                .Select(y_tis => Math.Pow(y_tis - averageY_tis, 2))
                .Sum();

                double SigmaY_tis = Math.Sqrt(sumSquaredDeviationsY / y_tisValues.Length);

                double threeSigmaY_tis = 3 * SigmaY_tis;

                recipeAnalysisItem.TIS_3Sigma_Y = threeSigmaY_tis;

                //grouped values of x_misreg and y_misreg
                var groupedValuesOfXAndYMisreg = lotArol.Measurements
                .Where(measurement => measurement.ArolMeasurement != null)
                .GroupBy(measurement => new
                {
                    measurement.Test_Num,
                    measurement.X_Die,
                    measurement.Y_Die
                })
                .Select(group => new
                {
                    GroupKey = group.Key,
                    SampleNumber = group.Count(),
                    ThreeSigmaX = CalculateThreeSigma(group.Select(measurement => measurement.ArolMeasurement?.X_Misreg).ToList()),
                    ThreeSigmaY = CalculateThreeSigma(group.Select(measurement => measurement.ArolMeasurement?.Y_Misreg).ToList())
                });

                // Calculate Precision_X,  Precision_Y
                double Precision_X = Math.Pow(groupedValuesOfXAndYMisreg.Sum(group => group.ThreeSigmaX ?? 0), 2) / groupedValuesOfXAndYMisreg.Sum(group => group.SampleNumber);

                double Precision_Y = Math.Pow(groupedValuesOfXAndYMisreg.Sum(group => group.ThreeSigmaY ?? 0), 2) / groupedValuesOfXAndYMisreg.Sum(group => group.SampleNumber);

                //Calculate TMU_X, TMU_Y TMU_Total
                recipeAnalysisItem.TMU_X = Math.Sqrt(Math.Pow(recipeAnalysisItem.TIS_Mean_X, 2) + Math.Pow(recipeAnalysisItem.TIS_3Sigma_X, 2) + Math.Pow(recipeAnalysisItem.Precision_X, 2));

                recipeAnalysisItem.TMU_Y = Math.Sqrt(Math.Pow(recipeAnalysisItem.TIS_Mean_Y, 2) + Math.Pow(recipeAnalysisItem.TIS_3Sigma_Y, 2) + Math.Pow(recipeAnalysisItem.Precision_Y, 2));

                recipeAnalysisItem.TMU_Total = (recipeAnalysisItem.TMU_X + recipeAnalysisItem.TMU_Y) / 2;

                //adding item to the list
                DataItems.Add(recipeAnalysisItem);
            }
        }

        // Event handler for property changed in the 'reportViewModel'
        public async void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            // Invoke a state change to update the UI
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        // Dispose method to clean up resources
        public void Dispose()
        {
            // Unsubscribe from the PropertyChanged event of the 'AnalysisViewModel'
            AnalysisViewModel.PropertyChanged -= OnPropertyChangedHandler;
        }

        private double? CalculateThreeSigma(List<double?> values)
        {
            // Filter out null values
            var filteredValues = values.Where(value => value.HasValue).Select(value => value.Value);

            if (filteredValues.Any())
            {
                // Calculate mean
                double mean = filteredValues.Average();

                // Calculate standard deviation
                double sumOfSquares = filteredValues.Sum(value => Math.Pow(value - mean, 2));

                double variance = sumOfSquares / (filteredValues.Count() - 1); // Using (n-1) for sample standard deviation

                double standardDeviation = Math.Sqrt(variance);

                // Calculate 3 sigma
                double threeSigma = 3 * standardDeviation;

                return threeSigma;
            }

            return null; // Return null if there are no valid values
        }

        // This method retrieves analysis data for a given recipe ID.
        private async Task GetAnalysisDataOfRecipe(Guid id)
        {
            // Check if analysis data for the specified recipe ID is already loaded.
            if (AnalysisViewModel.AnalysisDataByRecipeId == null)
            {
                // If not loaded, fetch analysis data for the specified recipe ID.
                await AnalysisViewModel.GetAnalysisDataByRecipeId(id);
            }
            else if (AnalysisViewModel.AnalysisDataByRecipeId.Recipe_Run_Id != id)
            {
                // If data is loaded but not for the specified recipe ID, reset the loaded data.
                AnalysisViewModel.AnalysisDataByRecipeId = null;

                // Fetch analysis data for the specified recipe ID.
                await AnalysisViewModel.GetAnalysisDataByRecipeId(id);
            }

            // Clear the DataItems list after fetching or resetting the data.
            DataItems.Clear();
        }

        // This method toggles the filter for analysis data items based on property values.
        public void ToggleFilter()
        {
            FilteredDataItems.Clear();

            // Check if the filter is currently active.
            if (!IsFilter == true)
            {
                // iterate through all data items.
                foreach (var item in DataItems)
                {
                    // Check if any property of the item is out of range.
                    if (IsAnyPropertyOutOfRange(item))
                    {
                        // If any property is out of range, add the item to the filtered list.
                        FilteredDataItems.Add(item);
                    }
                }
            }

            IsFilter = !IsFilter; // Toggle the filter state (activate or deactivate).
        }

        // Returns true if a numeric value is outside the specified range.
        public bool IsOutOfRange(double value, double? minValue, double? maxValue)
        {
            // Check if either minValue or maxValue is null, indicating an undefined range.
            if (minValue == null || maxValue == null)
            {
                // If either limit is null, the value is considered within the range.
                return false;
            }

            // Check if the value is less than minValue or greater than maxValue.
            return value < minValue || value > maxValue;
        }

        // Checks if any property values of a RecipeDataAnalysisItem fall outside their specified ranges.
        // Uses the provided rangeViewModel's selected ranges to determine the valid range for each property.
        // Returns true if any property value is outside its valid range, otherwise returns false.
        public bool IsAnyPropertyOutOfRange(RecipeDataAnalysisItem item)
        {
            // Check if any property value is outside its valid range.
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

        public async Task OnExportClick()
        {
            var exportInitName = "";

            // Define the column names for the export file.
            List<string> columnNames = Constants.AnalysisViewPage.SecondTableHeaders;

            // Stores values for each row of data.
            List<string> rowValues = new List<string>();

            // Create a StringBuilder to build the CSV content.
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(",", columnNames));

            if (DataItems != null)
            {
                foreach (var recipeAnalysisItem in DataItems)
                {
                    if (recipeAnalysisItem != null)
                    {

                        // Initialize rowValues with data from the recipeAnalysisItem.
                        // Map data to the corresponding columns
                        foreach (var columnName in columnNames)
                        {
                            switch (Constants.AnalysisViewPage.ExportDict[columnName])
                            {
                                case ExportRecipeAnalysisTitles.Date:
                                    rowValues.Add(" " + recipeAnalysisItem.RunDate.ToShortDateString());
                                    break;
                                case ExportRecipeAnalysisTitles.Lot:
                                    rowValues.Add(recipeAnalysisItem.LotName);
                                    break;
                                case ExportRecipeAnalysisTitles.TIS_Mean_X:
                                    rowValues.Add(recipeAnalysisItem.TIS_Mean_X.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.TIS_Mean_Y:
                                    rowValues.Add(recipeAnalysisItem.TIS_Mean_Y.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.TIS_3a_X:
                                    rowValues.Add(recipeAnalysisItem.TIS_3Sigma_X.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.TIS_3a_Y:
                                    rowValues.Add(recipeAnalysisItem.TIS_3Sigma_Y.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.Prec_X:
                                    rowValues.Add(recipeAnalysisItem.Precision_X.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.Prec_Y:
                                    rowValues.Add(recipeAnalysisItem.Precision_Y.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.TMU_X:
                                    rowValues.Add(recipeAnalysisItem.TMU_X.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.TMU_Y:
                                    rowValues.Add(recipeAnalysisItem.TMU_Y.ToString());
                                    break;
                                case ExportRecipeAnalysisTitles.TMU_Total:
                                    rowValues.Add(recipeAnalysisItem.TMU_Total.ToString());
                                    break;
                            }

                        }

                        // Add the row data to the output.
                        output.AppendLine(string.Join(",", rowValues));

                        // Clear rowValues for the next iteration.
                        rowValues.Clear();
                    }

                    exportInitName = recipeAnalysisItem.LotName;
                }

                // Convert the CSV content to a MemoryStream.
                using var stream = new MemoryStream(Encoding.Default.GetBytes(output.ToString()));

                // Save the stream as a CSV file asynchronously.
                var path = await FileSaver.SaveAsync(exportInitName + ".csv", stream, CancellationToken.None);
            }
        }
    }
}
