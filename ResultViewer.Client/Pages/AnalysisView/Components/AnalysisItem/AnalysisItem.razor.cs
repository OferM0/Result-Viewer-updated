using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Constants;

namespace ResultViewer.Client.Pages.AnalysisView.Components.AnalysisItem
{
    public partial class AnalysisItem
    {
        [Inject]
        public ConstantsBase Constants { get; set; } // Injects an instance of ConstantsBase for configuration constants

        [Parameter]
        public Core.Entities.Local.AnalysisItem item { get; set; } // Represents an AnalysisItem passed as a parameter to the component

        [Parameter]
        public EventCallback<Core.Entities.Local.AnalysisItem> OnDoubleClick { get; set; } // Event callback for double-click action

        // Method triggered when an AnalysisItem is double-clicked
        public async Task OnAnalysisItemDoubleClick()
        {
            await OnDoubleClick.InvokeAsync(item); // Invokes the double-click event callback with the AnalysisItem as an argument
        }
    }
}
