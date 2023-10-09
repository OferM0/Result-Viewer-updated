using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Core.Entities.Local;
using ResultViewer.Client.Core.Enums;

namespace ResultViewer.Client.Core.Components.ContnetContainer
{
    public partial class ContentContainer
    {
        [Inject]
        public ConstantsBase ConstantsManager { get; set; } // Inject constants for configuration

        [Parameter]
        public string Height { get; set; } = "80vh"; // Height of the container

        [Parameter]
        public string Width { get; set; } = "100%"; // Width of the container

        [Parameter]
        public List<string> Headers { get; set; } // List of headers

        [Parameter]
        public Guid LotId { get; set; } // Lot identifier

        [Parameter]
        public List<object> Items { get; set; } // List of items to display

        public List<object> CurrentItems { get; set; } // List of currently displayed items (for pagination)

        [Parameter]
        public bool IsPaginated { get; set; } = true; // Flag for pagination

        [Parameter]
        public bool IsTMUTable { get; set; } = false; // Flag for TMU table

        [Parameter]
        public ContentViewType ViewType { get; set; } // Type of content view

        [Parameter]
        public List<string> DropDownOptions { get; set; } // List of dropdown options

        [Parameter]
        public object SelectedDropDownOption { get; set; } // Selected dropdown option

        [Parameter]
        public bool IsCloseButtonEnabled { get; set; } = false; // Flag for close button

        [Parameter]
        public EventCallback<string> OnDropDownChange { get; set; } // Event callback for dropdown change

        [Parameter]
        public EventCallback<Guid> OnItemDoubleClick { get; set; } // Event callback for item double-click

        [Parameter]
        public EventCallback<AnalysisItem> OnAnalysisItemDoubleClick { get; set; } // Event callback for analysis item double-click

        [Parameter]
        public EventCallback OnCloseButtonClick { get; set; } // Event callback for close button click

        [Parameter]
        public EventCallback<Guid> OnExportClicked { get; set; } // Event callback for export button click

        // Variables for pagination
        int RowsPerPage;
        int CurrentPage = 1;
        int TotalRows = 0;

        // Initialize the component
        protected override async void OnInitialized()
        {
            RowsPerPage = int.Parse(ConstantsManager.PaginationComponent.RowsPerPage);
            TotalRows = Items.Count;
            UpdateCurrentData();
        }

        // Update the currently displayed data based on pagination
        public void UpdateCurrentData()
        {
            CurrentItems = Items.Skip((CurrentPage - 1) * RowsPerPage).Take(RowsPerPage).ToList();
            StateHasChanged();
        }

        // Handle page change event
        public async Task OnPageChanged(int page)
        {
            CurrentPage = page;
            UpdateCurrentData();
        }

        // Handle dropdown change event
        public async Task HandleDropDownChange(string option)
        {
            await OnDropDownChange.InvokeAsync(option);
        }

        // Handle item double-click event
        public async Task HandleItemDoubleClick(Guid id)
        {
            await OnItemDoubleClick.InvokeAsync(id);
        }

        // Handle analysis item double-click event
        public async Task HandleAnalysisItemDoubleClick(AnalysisItem item)
        {
            await OnAnalysisItemDoubleClick.InvokeAsync(item);
        }

        // Handle close button click event
        public async Task HandleOnCloseButtonClick()
        {
            await OnCloseButtonClick.InvokeAsync();
        }

        // Handle export button click event
        public async Task OnExportClick(Guid Run_Id)
        {
            await OnExportClicked.InvokeAsync(Run_Id);
        }
    }
}
