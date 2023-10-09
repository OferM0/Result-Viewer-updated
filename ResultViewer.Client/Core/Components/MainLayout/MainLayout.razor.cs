using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Components.MainLayout
{
    public partial class MainLayout
    {
        public bool _iconMenuActive { get; set; } = false; // Flag to control the visibility of the menu

        // Conditional CSS class based on the value of _iconMenuActive
        public string? IconMenuCssClass => _iconMenuActive ? null : "width: 80px; transition: width 0.6s ease-in-out; border-radius: 0px 0px 0px 0px;";

        // Method to toggle the visibility of the menu
        protected void ToggleIconMenu(bool iconMenuActive)
        {
            _iconMenuActive = iconMenuActive;
        }
    }
}
