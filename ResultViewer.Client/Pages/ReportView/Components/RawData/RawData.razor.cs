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

namespace ResultViewer.Client.Pages.ReportView.Components.RawData
{
	public partial class RawData
	{
        [Inject]
        public ConstantsBase Constants { get; set; } // Injected instance of ConstantsBase for configuration

        List<string> Headers { get; set; } // List of headers for raw data table

        [Inject]
        IReportViewModel reportViewModel { get; set; } // Injected instance of IReportViewModel for managing raw data

        [Parameter]
        public Guid LotId { get; set; } // Parameter representing the Lot ID

        [Parameter]
        public List<RawDataItem> Items { get; set; } = new List<RawDataItem>(); // List of raw data items for rendering

        GridSort<RawDataItem> SortByTestNun = GridSort<RawDataItem>
        .ByAscending(i => int.Parse(i.TestNum)); // Grid sorting configuration based on TestNum

        // Method called when the component is initialized
        protected override async Task OnInitializedAsync()
        {
            Headers = Constants.ReportViewPage.RawData.RawDataTitles; // Initialize headers from Constants

            // Find the corresponding LotRun for the given LotId
            var item = reportViewModel.LotRuns.Where(i => i.Run_Id == LotId).FirstOrDefault();

            if (item != null)
            {
                // Fetch measurements by LotId from the reportViewModel
                await reportViewModel.GetMeasurementsByLotId(item.Run_Id);
                var measurements = reportViewModel.MeasurementsByLotId;
                if (measurements != null)
                {
                    // Move items from measurements to Items list
                    foreach (var measurement in measurements)
                    {
                        if(measurement != null)
                        {
                            RawDataItem rawDataItem = new RawDataItem()
                            {

                                TestNum = measurement.Test_Num.ToString(),
                                TestType = "",
                                FieldX = measurement.X_Die.ToString(),
                                FieldY = measurement.Y_Die.ToString(),
                                ArrayX = measurement.X_Element.ToString(),
                                ArrayY = measurement.Y_Element.ToString(),
                                MisregX = measurement.ArolMeasurement?.X_Misreg.ToString() ?? "",
                                MisregY = measurement.ArolMeasurement?.Y_Misreg.ToString() ?? "",
                                TISXCorrection = measurement.ArolMeasurement?.X_Tis.ToString() ?? "",
                                TISYCorrection = measurement.ArolMeasurement?.Y_Tis.ToString() ?? "",
                                PassOrFail = measurement.Fail_Reason == "Ok"? "Passed":"Failed",
                                FailReason = measurement.Fail_Reason == "Ok" ? "": measurement.Fail_Reason,
                                PasymOvlX = "",
                                PasymOvlY = ""
                            };

                            Items.Add(rawDataItem); // Add the raw data item to the Items list
                        }
                    }
                }
            }
        }
    }
}
