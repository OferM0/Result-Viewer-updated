using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ResultViewer.Client.Core.Components.LotItem
{
    public partial class LotItem
    {
        [Inject]
        public ConstantsBase Constants { get; set; } // Inject constants for configuration

        [Inject]
        public IReportViewModel ReportViewModel { get; set; } // Inject a view model for reports

        [Parameter]
        public ReportItem item { get; set; } // Input parameter representing a report item

        public bool IsClicked { get; set; } = false; // Flag to track whether the item is clicked

        [Parameter]
        public EventCallback<Guid> OnDoubleClick { get; set; } // Event callback for double-clicking an item

        [Parameter]
        public EventCallback<Guid> OnExportClicked { get; set; } // Event callback for exporting an item

        private bool showMenu = false; // Flag to control the visibility of a menu

        // Method to get the status icon based on the status string
        public string GetStatusIcon(string status)
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

        // Method to handle double-clicking an item
        public async Task OnItemDoubleClick()
        {
            await OnDoubleClick.InvokeAsync(item.Run_Id);
        }

        // Method to toggle the click state of the item
        public void OnItemClick()
        {
            IsClicked = !IsClicked;
        }

        // Method to show or hide a menu
        private void ShowMenu(bool displayMenu)
        {
            showMenu = displayMenu;
        }

        // Method to handle exporting an item
        public async Task OnExportClick(Guid Run_Id)
        {
            await OnExportClicked.InvokeAsync(Run_Id);
        }
    }
}
