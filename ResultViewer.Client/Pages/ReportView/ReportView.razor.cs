using CommunityToolkit.Maui.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.Core.Enums;
using ResultViewer.Client.ViewModels;
using System.ComponentModel;
using System.Text;

namespace ResultViewer.Client.Pages.ReportView
{
    public partial class ReportView : IDisposable
    {
        [Inject]
        public ConstantsBase Constants { get; set; }  // Injected instance of ConstantsBase for configuration

        [Inject]
        IReportViewModel reportViewModel { get; set; } // Injected instance of RangeViewModel for managing data

        [Inject]
        IFileSaver fileSaver { get; set; }  // Injected instance of IFileSaver for export files

        public List<BreadcrumbItem> Breadcrumbs { get; set; } // Stores breadcrumb navigation items.

        public List<string> FailureLables = new List<string>(); //for failure summary graph
        public List<double> Values = new List<double>(); //for pass/fail graph
        public List<double> FailureValues = new List<double>(); //for failure summary graph

        public List<ReportItem> DataItems = new List<ReportItem>();

        public Guid LotId; // Stores the unique identifier for the lot

        // Dictionary to store sort options for lot runs.
        public Dictionary<string, LotRunSortInput> SortOptions { get; set; }

        // List to store lot run sort inputs.
        public List<LotRunSortInput> lotRunSortInputs = new List<LotRunSortInput>();

        // Pagination state for managing the number of items displayed per page.
        PaginationState Pagination;

        // Sort options for different properties of the ReportItem.
        GridSort<ReportItem> SortByName = GridSort<ReportItem>
        .ByAscending(i => i.LotName);
        GridSort<ReportItem> SortByRecipeName = GridSort<ReportItem>
        .ByAscending(i => i.RecipeName);
        GridSort<ReportItem> SortByStatus = GridSort<ReportItem>
        .ByAscending(i => i.LotStatus);
        GridSort<ReportItem> SortByRunTime = GridSort<ReportItem>
        .ByAscending(i => double.Parse(i.RunTime));

        // This method is executed when the component is initialized.
        protected override async Task OnInitializedAsync()
        {
            // Initialize breadcrumbs list, sort options and pagination
            Breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = Constants.SidebarComponent.Views.Report, Url = Constants.ReportViewPage.Url },
            };

            Pagination = new PaginationState { ItemsPerPage = int.Parse(Constants.PaginationComponent.RowsPerPage) };
            
            SortOptions = new Dictionary<string, LotRunSortInput>
            {
                { Constants.ReportViewPage.SortingDropDownOptions.SortBy, new LotRunSortInput {  } },
                { Constants.ReportViewPage.SortingDropDownOptions.ByRecent, new LotRunSortInput { Run_End_Time = SortEnumType.Desc } },
                { Constants.ReportViewPage.SortingDropDownOptions.ByName, new LotRunSortInput { Lot_Name = SortEnumType.Asc } },
                { Constants.ReportViewPage.SortingDropDownOptions.ByStatus, new LotRunSortInput { Lot_Status = SortEnumType.Desc } }
            };

            // Register an event handler for property changes in reportViewModel.
            reportViewModel.PropertyChanged += OnPropertyChangedHandler;

            // Initialize deafault sort option
            lotRunSortInputs.Add(SortOptions[Constants.ReportViewPage.SortingDropDownOptions.ByRecent]);

            await GetLotRuns();

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

        
        private async Task GetLotRuns()
        {
            if (reportViewModel.LotRuns == null)
            {
                await reportViewModel.GetAllLotRuns(lotRunSortInputs);

            }
            DataItems.Clear();

            // Dictionary to store fail reasons and their counts
            Dictionary<string, int> failureCounts = new Dictionary<string, int>();

            foreach (var lot in reportViewModel.LotRuns)
            {
                ReportItem item = new ReportItem();
                item.Run_Id = lot.Run_Id;
                item.Recipe_Run_Id = lot.Recipe_Run_Id;
                item.LotName = lot.Lot_Name;
                item.RecipeName = lot.Recipe_Name;

                // Convert Unix timestamp to DateTime object
                double unixTimestamp = lot.Run_Start_Time;
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp);
                item.StartRunDate = dateTimeOffset.DateTime;

                // Convert Unix timestamp to DateTime object
                unixTimestamp = lot.Run_End_Time;
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)unixTimestamp);
                item.EndRunDate = dateTimeOffset.DateTime;

                // Calculate the runtime in minutes
                TimeSpan runtime = item.EndRunDate - item.StartRunDate;
                float runtimeMinutes = (float)runtime.TotalMinutes;
                item.RunTime = runtimeMinutes.ToString();

                // Convert integer to boolean
                int lotStatusValue = Convert.ToInt32(lot.Lot_Status);
                bool status = lotStatusValue == 0;
                item.LotStatus = status ? Constants.ReportViewPage.StatusPassed : Constants.ReportViewPage.StatusFailed; //0- passed, 1- failed

                if (lot.Measurements != null)
                {
                    if (item.LotStatus == Constants.ReportViewPage.StatusFailed)
                    {
                        item.Reason = lot.Measurements.FirstOrDefault(m => m.Fail_Reason.ToString() != "Ok").Fail_Reason;
                    }
                    else
                    {
                        item.Reason = lot.Measurements.First().Fail_Reason;
                    }
                }
                else
                {
                    item.Reason = status ? "Ok" : "Error";
                }

                // Iterate through Measurements collection and retrieve Fail_Reason
                if (item.LotStatus == Constants.ReportViewPage.StatusFailed && lot.Measurements != null)
                {

                    var measurement = lot.Measurements.FirstOrDefault(m => m.Fail_Reason != null && m.Fail_Reason.ToString() != string.Empty && m.Fail_Reason.ToString() != "Ok");
                    string failReason = measurement.Fail_Reason;

                    // If the failure reason is already in the dictionary, increment its count
                    if (failureCounts.ContainsKey(failReason))
                    {
                        failureCounts[failReason]++;
                    }
                    // If the failure reason is not in the dictionary, add it with count 1
                    else if (failReason != "0")
                    {
                        failureCounts.Add(failReason, 1);
                    }
                }

                DataItems.Add(item);
            }

            Values = new List<double>
            {
                DataItems.Cast<ReportItem>().Count(item => item.LotStatus == Constants.ReportViewPage.StatusPassed),
                DataItems.Cast<ReportItem>().Count(item => item.LotStatus == Constants.ReportViewPage.StatusFailed)
            };

            // Clear existing lists before populating them with failure labels and values
            FailureLables.Clear();
            FailureValues.Clear();

            // Populate the lists with failure labels and values based on the dictionary
            foreach (var kvp in failureCounts)
            {
                FailureLables.Add(kvp.Key);
                FailureValues.Add(kvp.Value);
            }
        }

        public async Task OnLotItemDoubleClick(Guid run_Id)
        {
            openRowId = "";
            LotId = run_Id;
            string lotName = DataItems.FirstOrDefault(i => i.Run_Id == run_Id).LotName;
            
            // Find the index of the last forward slash
            int lastSlashIndex = lotName.LastIndexOf(@"\");
            
            // Extract the substring after the last forward slash
            lotName = lotName.Substring(lastSlashIndex + 1);

            string newUrl = $"report/{lotName}";
            Breadcrumbs.Add(new BreadcrumbItem { Title = lotName, Url = newUrl });
            
            // Serialize the DateRangeInfo object to JSON
            string breadCrumbsInJson = JsonConvert.SerializeObject(Breadcrumbs);

            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                { "lotId", run_Id.ToString() },
                {"BreadCrumbs",  breadCrumbsInJson}
            };

            NavManager.NavigateTo(QueryHelpers.AddQueryString(newUrl, queryParams));
        }

        //return match icon based on status
        public static string GetStatusIcon(string status)
        {
            if (status == "Passed")
            {
                return Icons.GreenDot;
            }
            else if (status == "Failed")
            {
                return Icons.RedDot;
            }
            else
            {
                return string.Empty;
            }
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

        public async Task OnExportClick(Guid Run_Id)
        {    
            // Define the column names for the export file.
            List<string> columnNames = Constants.ReportViewPage.ExportTitles;

            // Stores values for each row of data.
            List<string> rowValues = new List<string>();

            // Create a StringBuilder to build the CSV content.
            StringBuilder output = new StringBuilder();
            output.AppendLine(string.Join(",", columnNames));

            // Retrieve measurements by the provided Run_Id.
            await reportViewModel.GetMeasurementsByLotId(Run_Id);
            var measurements = reportViewModel.MeasurementsByLotId;
            
            if (measurements != null)
            {
                foreach (var measurement in measurements)
                {
                    if (measurement != null)
                    {
                        //await reportViewModel.GetMeasurementById(measurement.Measurement_Id);
                        var measurementById = measurement;

                        // Initialize rowValues with data from the measurement.
                        // Map measurement data to the corresponding columns
                        foreach (var columnName in columnNames)
                        {
                            switch (Constants.ReportViewPage.ExportDict[columnName])
                            {
                                case ExportLotTitles.DateTime:
                                    rowValues.Add(DateTimeOffset.FromUnixTimeSeconds((long)measurementById.LotRun.Run_End_Time).DateTime.ToString());
                                    break;
                                case ExportLotTitles.Lot:
                                    rowValues.Add(measurementById.LotRun.Lot_Name);
                                    break;
                                case ExportLotTitles.Operator:
                                    rowValues.Add(measurementById.LotRun.Operator_Name);
                                    break;
                                case ExportLotTitles.Program:
                                    rowValues.Add(measurementById.LotRun.Recipe_Name);
                                    break;
                                case ExportLotTitles.SlotNum:
                                    rowValues.Add(measurementById.Slot_Num.ToString());
                                    break;
                                case ExportLotTitles.WaferNum:
                                    rowValues.Add(measurementById.Wafer_Ordinal_Num.ToString());
                                    break;
                                case ExportLotTitles.WaferID:
                                    rowValues.Add(measurementById.Wafer_Id);
                                    break;
                                case ExportLotTitles.XDieSize:
                                    rowValues.Add(measurementById.LotRun.RecipeRun != null ? measurementById.LotRun.RecipeRun.Die_Size_X.ToString() : "");
                                    break;
                                case ExportLotTitles.YDieSize:
                                    rowValues.Add(measurementById.LotRun.RecipeRun != null ? measurementById.LotRun.RecipeRun.Die_Size_Y.ToString() : "");
                                    break;
                                case ExportLotTitles.TestType:
                                    rowValues.Add("AROL");
                                    break;
                                case ExportLotTitles.TestLabel:
                                    rowValues.Add(measurementById.Test_Label);
                                    break;
                                case ExportLotTitles.TestID:
                                    rowValues.Add("");
                                    break;
                                case ExportLotTitles.LocationX:
                                    rowValues.Add(measurementById.X_Location.ToString());
                                    break;
                                case ExportLotTitles.LocationY:
                                    rowValues.Add(measurementById.Y_Location.ToString());
                                    break;
                                case ExportLotTitles.FieldX:
                                    rowValues.Add(measurementById.X_Die.ToString());
                                    break;
                                case ExportLotTitles.FieldY:
                                    rowValues.Add(measurementById.Y_Die.ToString());
                                    break;
                                case ExportLotTitles.ArrayX:
                                    rowValues.Add(measurementById.X_Element.ToString());
                                    break;
                                case ExportLotTitles.ArrayY:
                                    rowValues.Add(measurementById.Y_Element.ToString());
                                    break;
                                case ExportLotTitles.TargetLocationX:
                                    rowValues.Add(measurementById.ArolMeasurement != null ? measurementById.ArolMeasurement.X_Tis.ToString() : "");
                                    break;
                                case ExportLotTitles.TargetLocationY:
                                    rowValues.Add(measurementById.ArolMeasurement != null ? measurementById.ArolMeasurement.Y_Tis.ToString() : "");
                                    break;
                                case ExportLotTitles.WaferLocationX:
                                    rowValues.Add((measurementById.Wafer_X / 10000000).ToString());
                                    break;
                                case ExportLotTitles.WaferLocationY:
                                    rowValues.Add((measurementById.Wafer_Y / 10000000).ToString());
                                    break;
                                case ExportLotTitles.MregX:
                                    rowValues.Add(measurementById.ArolMeasurement != null ? measurementById.ArolMeasurement.X_Misreg.ToString() : "");
                                    break;
                                case ExportLotTitles.MregY:
                                    rowValues.Add(measurementById.ArolMeasurement != null ? measurementById.ArolMeasurement.Y_Misreg.ToString() : "");
                                    break;
                            }

                        }

                        // Add the row data to the output.
                        output.AppendLine(string.Join(",", rowValues));

                        // Clear rowValues for the next iteration.
                        rowValues.Clear();
                    }
                }
            }

            // Convert the CSV content to a MemoryStream.
            using var stream = new MemoryStream(Encoding.Default.GetBytes(output.ToString()));

            // Define the initial file name for saving.
            string fileInitialName = $"ResultLotRun.csv";

            // Save the stream as a CSV file asynchronously.
            var path = await fileSaver.SaveAsync(fileInitialName, stream, CancellationToken.None);

            //string directoryPath = @"C:\CSV_File\";
            //string filePath = Path.Combine(directoryPath, Run_Id.ToString() + ".csv");

            // Create the directory if it doesn't exist
            //if (!Directory.Exists(directoryPath))
            //{
            //    Directory.CreateDirectory(directoryPath);
            //}

            //System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}
