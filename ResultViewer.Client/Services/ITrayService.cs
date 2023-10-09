using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Services
{
    // Define an interface for a TrayService
    public interface ITrayService
    {
        // This method is used to initialize the TrayService, typically for platform-specific setup. (windows)
        void Initialize();

        // This property represents an action that can be set to handle a click event in the system tray.
        // It allows you to define a callback method that gets executed when the system tray icon is clicked.
        Action ClickHandler { get; set; }
    }
}
