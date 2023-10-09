using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Components.FloatingCloseButton
{
    public partial class FloatingCloseButton
    {
        [Parameter]
        public EventCallback OnCloseClick { get; set; } // Event callback for closing

        // Method to handle the click event for closing
        public async Task OnCloseClickHandler()
        {
            await OnCloseClick.InvokeAsync();
        }
    }
    
}
