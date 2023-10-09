using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Constants.Views;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.Core.Enums;
using ResultViewer.Client.Pages.AnalysisView;
using ResultViewer.Client.ViewModels;
using System.ComponentModel;

namespace ResultViewer.Client.Pages.ReportView
{
    public partial class LotReport : IDisposable
    {
        [Inject]
        IReportViewModel reportViewModel { get; set; } // Injected instance of RangeViewModel for managing data

        [Inject]
        public ConstantsBase Constants { get; set; } // Injected instance of ConstantsBase for configuration

        public List<BreadcrumbItem> Breadcrumbs { get; set; } // Stores breadcrumb navigation items.

        public Dictionary<string, ReportViewDropDownOptions> NavigationOptions { get; set; } // Stores navigation dropdown options.

        // Stores the currently selected navigation dropdown option.
        public ReportViewDropDownOptions SelectedReportViewDropDownOption { get; set; }

        public List<string> Headers { get; set; } // Stores table headers

        public List<SummaryItem> LotSummaryItems = new List<SummaryItem>();

        public List<RawDataItem> RawDataItem = new List<RawDataItem>();

        public Guid LotId; // Stores the unique identifier for the lot

        [Parameter] public string LotName { get; set; }

        // This method is executed when the component is initialized.
        protected override async Task OnInitializedAsync()
        {
            // Initialize navigation options, headers, and default dropdown selection.
            NavigationOptions = Constants.ReportViewPage.NavigationDropDownOptions;
            Headers = GetRelevantTitles("Error Summary");
            SelectedReportViewDropDownOption = ReportViewDropDownOptions.ErrorSummary;

            // Register an event handler for property changes in reportViewModel.
            reportViewModel.PropertyChanged += OnPropertyChangedHandler;

            // Parse query parameters from the URL.
            Dictionary<string, StringValues> querystring = QueryHelpers.ParseQuery(NavManager.ToAbsoluteUri(NavManager.Uri).Query);

            if (querystring.TryGetValue("BreadCrumbs", out var breadCrumbsInJson))
            {
                // Deserialize breadcrumb items from JSON
                Breadcrumbs = JsonConvert.DeserializeObject<List<BreadcrumbItem>>(breadCrumbsInJson);
            }

            if (querystring.TryGetValue("lotId", out var lotId))
            {
                try
                {
                    string run_id = lotId;
                    await OnLotItemDoubleClick((Guid.Parse(run_id)));
                }
                catch (JsonException)
                {
                    // Handle JSON deserialization error
                    // ...
                }
            }

            await base.OnInitializedAsync();
        }

        // This event handler is triggered when a property in reportViewModel changes.
        public async void OnPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            await InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        // Dispose of the component and unregister the property change event handler.
        public void Dispose()
        {
            reportViewModel.PropertyChanged -= OnPropertyChangedHandler;
        }

        public void HandleDropDownChange(string option)
        {
            // Update the table headers based on user selection.
            Headers = GetRelevantTitles(option);

            // Update the selected dropdown option based on user selection.
            switch (NavigationOptions[option])
            {
                case ReportViewDropDownOptions.LotSummary:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.LotSummary;
                    break;
                case ReportViewDropDownOptions.RawData:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.RawData;
                    break;
                case ReportViewDropDownOptions.ErrorSummary:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.ErrorSummary;
                    break;
                case ReportViewDropDownOptions.TISSummary:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.TISSummary;
                    break;
                case ReportViewDropDownOptions.OptimizerSummary:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.OptimizerSummary;
                    break;
                case ReportViewDropDownOptions.FieldStatistics:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.FieldStatistics;
                    break;
                case ReportViewDropDownOptions.SiteStatistics:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.SiteStatistics;
                    break;
                case ReportViewDropDownOptions.LotStatistics:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.LotStatistics;
                    break;
                case ReportViewDropDownOptions.WaferStatistics:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.WaferStatistics;
                    break;
                case ReportViewDropDownOptions.TestStatistics:
                    SelectedReportViewDropDownOption = ReportViewDropDownOptions.TestStatistics;
                    break;
            }
        }

        //public void HandleCloseButtonClick()
        //{
        //    //isItemSelected = false;
        //    Headers = GetRelevantTitles("Error Summary");
        //    SelectedReportViewDropDownOption = ReportViewDropDownOptions.ErrorSummary;
        //}

        // Return match table headers based on user selection.
        public List<string> GetRelevantTitles(string optionSelected)
        {            
            List<string> headLineList;

            ReportViewDropDownOptions option = NavigationOptions[optionSelected];

            switch (option)
            {
                case ReportViewDropDownOptions.LotSummary:
                    headLineList = Constants.ReportViewPage.LotSummary.MainHeaders;
                    break;

                case ReportViewDropDownOptions.RawData:
                    headLineList = Constants.ReportViewPage.RawData.MainHeaders;
                    break;

                case ReportViewDropDownOptions.FieldStatistics:
                    headLineList = new List<string> { "Field 1", "Field 2", "Field 3" };
                    break;

                case ReportViewDropDownOptions.LotStatistics:
                    headLineList = new List<string> { "Lot 1", "Lot 2", "Lot 3" };
                    break;

                case ReportViewDropDownOptions.SiteStatistics:
                    headLineList = new List<string> { "Site 1", "Site 2", "Site 3" };
                    break;

                case ReportViewDropDownOptions.TestStatistics:
                    headLineList = new List<string> { "Test 1", "Test 2", "Test 3" };
                    break;

                case ReportViewDropDownOptions.WaferStatistics:
                    headLineList = new List<string> { "Wafer 1", "Wafer 2", "Wafer 3" };
                    break;

                case ReportViewDropDownOptions.TISSummary:
                    headLineList = new List<string> { "TIS 1", "TIS 2", "TIS 3" };
                    break;

                case ReportViewDropDownOptions.ErrorSummary:
                    headLineList = new List<string> { "Error 1", "Error 2", "Error 3" };
                    break;

                case ReportViewDropDownOptions.OptimizerSummary:
                    headLineList = new List<string> { "Optimizer 1", "Optimizer 2", "Optimizer 3" };
                    break;

                default:
                    headLineList = new List<string> { "Title", "Value" };
                    break;
            }

            return headLineList;
        }

        public async Task OnLotItemDoubleClick(Guid run_Id)
        {
            LotId = run_Id;
            LotSummaryItems.Clear();
            //isItemSelected = true;

            // Get the item associated with the selected Lot ID.
            var item = reportViewModel.LotRuns.Where(i => i.Run_Id == run_Id).FirstOrDefault();

            if (item != null)
            {
                // Get recipe run details.
                await reportViewModel.GetRecipeRunById(item.Recipe_Run_Id.Value);
                var recipeRun = reportViewModel.RecipeRunById;

                if (recipeRun != null)
                {
                    // Calculate total run time and failure percentage.
                    TimeSpan totalRunTime = DateTimeOffset.FromUnixTimeSeconds((long)item.Run_End_Time).DateTime - DateTimeOffset.FromUnixTimeSeconds((long)item.Run_Start_Time).DateTime;
                    double failurePercentage = (double)((recipeRun.LotRuns.Where(l => l.Lot_Status == 0).Count()) / (double)recipeRun.LotRuns.Count()) * 100;

                    // Populate LotSummaryItems with summary information.
                    LotSummaryItems = new List<SummaryItem>
                    {
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Lot_ID],
                            Value = item.Lot_Name,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Recipe_Name],
                            Value = item.Recipe_Name,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Tool_ID],
                            Value = item.Tool_Id,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Operator_ID],
                            Value = item.Operator_Name,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Stepper_ID],
                            Value = item.Stepper_Id,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Cassette_ID],
                            Value = "",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Comments],
                            Value = item.Comments,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.TIS_Mode],
                            Value = item.Tis_Mode.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Calibration_Mode],
                            Value = item.Calibration_Mode.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Report_Date],
                            Value = DateTime.Now.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Lot_Start_Date],
                            Value = DateTimeOffset.FromUnixTimeSeconds((long)item.Run_Start_Time).DateTime.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Lot_End_Date],
                            Value = DateTimeOffset.FromUnixTimeSeconds((long)item.Run_End_Time).DateTime.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Total_Run_Time],
                            Value = totalRunTime.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Number_of_Measured_Sites],
                            Value = "",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Number_of_Wafers],
                            Value = "",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Number_of_Iterations],
                            Value = item.Iteration.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Number_of_Tests],
                            Value = "",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Failure_Percentage],
                            Value = $"~{failurePercentage}%",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Recipe_Version],
                            Value = recipeRun.Version_String,
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Recipe_Created],
                            Value = DateTimeOffset.FromUnixTimeSeconds((long)recipeRun.Recipe_Creation_Date).DateTime.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Recipe_Modified],
                            Value = DateTimeOffset.FromUnixTimeSeconds((long)recipeRun.Recipe_Modification_Date).DateTime.ToString(),
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Wafer_Size],
                            Value = recipeRun.Wafer_Size.ToString().Substring(0, 3) + " mm",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Wafer_Type],
                            Value = recipeRun.Wafer_Type.ToString() + " Notch"
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Wafer_Orientation],
                            Value = recipeRun.Wafer_Orientation.ToString() + " deg",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Field_Size],
                            Value = "",
                        },
                        new SummaryItem
                        {
                            Title = Constants.ReportViewPage.LotSummary.SummaryTitles[LotSummaryTitles.Number_of_Dies],
                            Value = "",
                        },
                    };
                }
            }
        }
    }
}
