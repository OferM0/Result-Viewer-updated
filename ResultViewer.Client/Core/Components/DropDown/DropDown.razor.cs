using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Components.DropDown
{
    public partial class DropDown
    {
        [Parameter]
        public List<string> Options { get; set; } // List of options to be displayed

        [Parameter]
        public EventCallback<string> OnChangeEvent { get; set; } // Event callback for handling a change in the selected option

        // Method to handle the change event when an option is selected
        public async Task HandleChangeEvent(ChangeEventArgs e)
        {
            await OnChangeEvent.InvokeAsync(e.Value.ToString());
        }
    }
}
