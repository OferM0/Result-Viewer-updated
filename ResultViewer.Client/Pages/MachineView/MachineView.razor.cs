using Microsoft.AspNetCore.Components.QuickGrid;
using ResultViewer.Client.Core.Components.Pagination;
using ResultViewer.Client.Core.Entities.Local;

namespace ResultViewer.Client.Pages.MachineView
{
    public partial class MachineView
    {
        public MachineItem MachineItem { get; set; } // Represents a single MachineItem
        public List<MachineItem> MachineItems { get; set; } // Represents a list of MachineItem instances
        public PaginationState Pagination { get; set; } // Represents the pagination state

        // Method called when the component is initialized
        protected override async Task OnInitializedAsync()
        {
            Pagination = new PaginationState { ItemsPerPage = 10 }; // Initialize the pagination state with 10 items per page

            MachineItems = new List<MachineItem>(); // Initialize the list of MachineItem instances

            // Fill the MachineItems list
            for (int i = 2; i <= 20; i++)
            {
                MachineItems.Add(new MachineItem
                {
                    Name = $"Machine {i}",
                    IpAddress = $"192.168.1.{i}",
                    Status = "Offline"
                });
            }

            // Initialize a single MachineItem instance
            MachineItem = new MachineItem { Name = "Machine 1", IpAddress = "192.168.1.1", Status = "Online" };
        }
    }
}
