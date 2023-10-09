using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using ResultViewer.Client.Core.Entities.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResultViewer.Client.Core.Components.Breadcrumb
{
    public partial class Breadcrumb
    {
        public Breadcrumb() { } // Default constructor

        [Parameter] public List<BreadcrumbItem> Breadcrumbs { get; set; } // List of breadcrumb items

        // Method to transport state by navigating to a specified URL and updating the breadcrumbs
        public void TransportState(string url)
        {
            Dictionary<string, string> vars = new Dictionary<string, string>();

            // Navigate to the URL with any query string parameters
            NavManager.NavigateTo(QueryHelpers.AddQueryString(url, vars));

            // Remove items from the breadcrumb list
            RemoveFromList(url);
        }

        // Method to remove breadcrumb items from the list until a specified URL is found
        private void RemoveFromList(string url)
        {
            while (Breadcrumbs.Last().Url != url)
            {
                Breadcrumbs.RemoveAt(Breadcrumbs.Count - 1);
            }
        }

        // Method to add a breadcrumb item to the list
        public void AddToList(BreadcrumbItem breadcrumb)
        {
            Breadcrumbs.Add(breadcrumb);
        }
    }
}
