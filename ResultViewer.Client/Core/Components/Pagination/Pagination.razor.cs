using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Components.Pagination
{
    public partial class Pagination
    {
        [Inject]
        public ConstantsBase Constants { get; set; } // Inject constants for configuration

        [Parameter] public int CurrentPage { get; set; } // Current page number
        [Parameter] public int RowsPerPage { get; set; } // Number of rows per page
        [Parameter] public int TotalRows { get; set; } // Total number of rows
        [Parameter] public EventCallback<int> OnPageChanged { get; set; } // Event callback for page change

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalRows, RowsPerPage)); // Calculate total pages
        public bool IsFirstPage => CurrentPage == 1; // Check if it's the first page
        public bool IsLastPage => CurrentPage == TotalPages; // Check if it's the last page

        // Method to navigate to a specific page based on user input
        public void GoToPage(ChangeEventArgs e)
        {
            int page = int.Parse(e.Value.ToString());
            if (page >= 1 && page <= TotalPages && page != CurrentPage)
            {
                CurrentPage = page;
                OnPageChanged.InvokeAsync(CurrentPage); // Invoke page change event
            }
        }

        // Method to navigate to the previous page
        public void GoToPreviousPage()
        {
            if (!IsFirstPage)
            {
                CurrentPage--;
                OnPageChanged.InvokeAsync(CurrentPage); // Invoke page change event
            }
        }

        // Method to navigate to the next page
        public void GoToNextPage()
        {
            if (!IsLastPage)
            {
                CurrentPage++;
                OnPageChanged.InvokeAsync(CurrentPage); // Invoke page change event
            }
        }
    }
}
