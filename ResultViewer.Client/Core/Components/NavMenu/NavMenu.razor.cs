using Microsoft.AspNetCore.Components;
using ResultViewer.Client.Core.Constants;

namespace ResultViewer.Client.Core.Components.NavMenu
{
    public partial class NavMenu
    {
        [Inject]
        public ConstantsBase Constants { get; set; }
        //bool to send to MainLayout for shrinking sidebar and showing/hide menu text
        public bool IconMenuActive { get; set; } = false;

        //EventCallback for sending bool to MainLayout
        [Parameter]
        public EventCallback<bool> ShowIconMenu { get; set; }

        //Method to toggle IconMenuActive bool and send bool via EventCallback
        public async Task ToggleIconMenu()
        {
            IconMenuActive = !IconMenuActive;
            await ShowIconMenu.InvokeAsync(IconMenuActive);
        }

        public async Task OpenFileDialog()
        {
            await FilePicker.PickAsync();
        }
    }
}
