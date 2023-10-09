using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using ResultViewer.Client.Core.Components.Pagination;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Constants.Views;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.ViewModels;
using System.ComponentModel;

namespace ResultViewer.Client.Pages.AnalysisView
{
    public partial class AnalysisView : IDisposable
    {
        // Inject the 'ConstantsBase' and 'IReportViewModel' dependencies
        [Inject]
        ConstantsBase Constants { get; set; }

        [Inject]
        IReportViewModel reportViewModel { get; set; }

        // Properties to hold the selected date range and a callback to handle date range selection
        DateTimeOffset? StartDate { get; set; }
        DateTimeOffset? EndDate { get; set; }
        public EventCallback<DateRangeInfo> OnSelectDateRange { get; set; }

        // List of data items for analysis
        public List<AnalysisItem> DataItems = new List<AnalysisItem>();

        // Property to hold the selected item
        public AnalysisItem SelectedItem { get; set; }

        // Dictionary to hold sorting options for the lot runs
        public Dictionary<string, LotRunSortInput> SortOptions { get; set; }

        // List to store the selected lot run sorting option(s)
        public List<LotRunSortInput> lotRunSortInputs = new List<LotRunSortInput>();

        // Flag to indicate if an item is selected
        public bool isItemSelected = false;

        // List of breadcrumb items for navigation
        public List<BreadcrumbItem> Breadcrumbs { get; set; }

        // Pagination state for managing the number of items displayed per page.
        PaginationState Pagination;

        // Sort options for different properties of the ReportItem.
        GridSort<AnalysisItem> SortByRecipeName = GridSort<AnalysisItem>
        .ByAscending(i => i.RecipeName);

        // Lifecycle method: executed when the component is initialized
        protected override async Task OnInitializedAsync()
        {
            // Initialize the breadcrumb list with a single item
            Breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = Constants.SidebarComponent.Views.Analysis, Url = Constants.AnalysisViewPage.Url },
            };

            // Initialize the pagination state
            Pagination = new PaginationState { ItemsPerPage = int.Parse(Constants.PaginationComponent.RowsPerPage) };

            // Initialize the sort options for lot runs
            SortOptions = new Dictionary<string, LotRunSortInput>
            {
                { Constants.ReportViewPage.SortingDropDownOptions.SortBy, new LotRunSortInput {  } },
                { Constants.ReportViewPage.SortingDropDownOptions.ByRecent, new LotRunSortInput { Run_End_Time = SortEnumType.Desc } },
                { Constants.ReportViewPage.SortingDropDownOptions.ByName, new LotRunSortInput { Lot_Name = SortEnumType.Asc } },
                { Constants.ReportViewPage.SortingDropDownOptions.ByStatus, new LotRunSortInput { Lot_Status = SortEnumType.Desc } }
            };

            // Subscribe to the PropertyChanged event of the 'reportViewModel'
            reportViewModel.PropertyChanged += OnPropertyChangedHandler;

            // Add the 'ByRecent' sorting option to the list of selected lot run sort inputs
            lotRunSortInputs.Add(SortOptions[Constants.ReportViewPage.SortingDropDownOptions.ByRecent]);

            // Fetch all recipes from the report view model
            await GetAllRecipes();

            // Call the base class's OnInitializedAsync method
            await base.OnInitializedAsync();
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
            // Unsubscribe from the PropertyChanged event of the 'reportViewModel'
            reportViewModel.PropertyChanged -= OnPropertyChangedHandler;
        }

        // Private method to fetch all recipes from the report view model
        private async Task GetAllRecipes()
        {
            // If the 'LotRuns' property in the 'reportViewModel' is null, fetch all lot runs
            if (reportViewModel.LotRuns == null)
            {
                await reportViewModel.GetAllLotRuns(lotRunSortInputs);
            }
            DataItems.Clear(); // Clear the list of data items

            // Create a dictionary to store the aggregated data for each recipe name
            Dictionary<string, AnalysisItem> recipeData = new Dictionary<string, AnalysisItem>();

            foreach (var lot in reportViewModel.LotRuns.Where(l => l.Recipe_Run_Id != null))
            {
                // Check if the recipe name exists in the dictionary
                if (recipeData.ContainsKey(lot.Recipe_Name))
                {
                    // Retrieve the existing AnalysisItem for the recipe name
                    AnalysisItem existingItem = recipeData[lot.Recipe_Name];

                    // Update FirstRunDate if the current lot's start date is earlier
                    double unixTimestamp = lot.Run_Start_Time;
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp);
                    if (dateTimeOffset.DateTime < existingItem.FirstRunDate)
                        existingItem.FirstRunDate = dateTimeOffset.DateTime;

                    unixTimestamp = lot.Run_End_Time;
                    dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp);
                    // Update LastRunDate if the current lot's end date is later
                    if (dateTimeOffset.DateTime > existingItem.LastRunDate)
                        existingItem.LastRunDate = dateTimeOffset.DateTime;

                    // Increment the count for the recipe name
                    existingItem.Count++;
                }
                else
                {
                    double unixTimestamp = lot.Run_Start_Time;
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp);

                    double unixTimestamp2 = lot.Run_End_Time;
                    DateTimeOffset dateTimeOffset2 = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp2);

                    // Create a new AnalysisItem for the recipe name
                    AnalysisItem newItem = new AnalysisItem
                    {
                        Recipe_Run_Id = lot.Recipe_Run_Id.HasValue ? lot.Recipe_Run_Id.Value : default,
                        RecipeName = lot.Recipe_Name,
                        FirstRunDate = dateTimeOffset.DateTime,
                        LastRunDate = dateTimeOffset2.DateTime,
                        Count = 1
                    };

                    // Add the new AnalysisItem to the dictionary
                    recipeData[lot.Recipe_Name] = newItem;
                }
            }

            // Add all AnalysisItems from the dictionary to the DataItems list
            DataItems.AddRange(recipeData.Values);
        }

        public async Task OnDoubleClick(AnalysisItem item)
        {
            openRowId = "";
            SelectedItem = item;
            StartDate = item.FirstRunDate;
            EndDate = item.LastRunDate;
            isItemSelected = true;
        }

        public async Task HandleDateRange(DateRangeInfo dateRangeInfo)
        {
            dateRangeInfo.StartDate = StartDate;
            dateRangeInfo.EndDate = EndDate;

            // Replace this with the actual recipe name variable or value
            string recipeName = dateRangeInfo.Item.RecipeName;
            // Find the index of the last forward slash
            int lastSlashIndex = recipeName.LastIndexOf(@"\");
            // Extract the substring after the last forward slash
            recipeName = recipeName.Substring(lastSlashIndex + 1);

            // Serialize the DateRangeInfo object to JSON
            string dateRangeInfoJson = JsonConvert.SerializeObject(dateRangeInfo);

            string newUrl = $"analysis/{recipeName}";
            Breadcrumbs.Add(new BreadcrumbItem { Title = recipeName, Url = newUrl });

            // Serialize the DateRangeInfo object to JSON
            string breadCrumbsInJson = JsonConvert.SerializeObject(Breadcrumbs);

            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "dateRangeInfo", dateRangeInfoJson },
                {"BreadCrumbs",  breadCrumbsInJson}
            };

            NavManager.NavigateTo(QueryHelpers.AddQueryString(newUrl, queryParams));
        }

        private string openRowId = "";

        private void ToggleDropdown(string rowId)
        {
            if (openRowId == rowId)
                openRowId = "";
            else
                openRowId = rowId;
        }

        private void CloseDropdown()
        {
            openRowId = "";
        }

        public void HandleOnCloseButtonClick()
        {
            isItemSelected = false;
        }
    }

    public class DateRangeInfo
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public AnalysisItem Item { get; set; }
    }
}
