using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.PieChart;
using ChartJs.Blazor.Util;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace ResultViewer.Client.Core.Components.Charts.PieCharts
{
    public partial class PieChart
    {
        public PieConfig _config; // Configuration for the pie chart

        [Parameter]
        public List<string> Lables { get; set; } = new List<string> { "Label1", "Label2" }; // List of labels for chart segments

        [Parameter]
        public List<double> Values { get; set; } = new List<double> { 100, 50 }; // List of values for chart segments

        [Parameter]
        public string Title { get; set; } = ""; // Title for the pie chart

        // Initialize the component asynchronously
        protected override async Task OnInitializedAsync()
        {
            _config = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    MaintainAspectRatio = false,
                    Legend = new Legend
                    {
                        Position = Position.Bottom
                    },
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = Title
                    },
                    CutoutPercentage = 50,
                    Rotation = -90,
                }
            };

            // Add labels to the chart configuration
            foreach (var label in Lables)
            {
                _config.Data.Labels.Add(label);
            }

            // Create a dataset for the chart with background colors
            PieDataset<double> dataset = new PieDataset<double>(Values)
            {
                BackgroundColor = new[]
                {
                    ColorUtil.ColorHexString(237, 125, 49),
                    ColorUtil.ColorHexString(91, 155, 213),
                    ColorUtil.ColorHexString(0, 200, 150),
                    ColorUtil.ColorHexString(220, 70, 60),
                    ColorUtil.ColorHexString(102, 204, 167),
                    ColorUtil.ColorHexString(204, 102, 102),
                    ColorUtil.ColorHexString(99,171,253),
                    ColorUtil.ColorHexString(22,91,170),
                    ColorUtil.ColorHexString(46,139,87),
                    ColorUtil.ColorHexString(255,69,0)
                }
            };

            // Add the dataset to the chart configuration
            _config.Data.Datasets.Add(dataset);
        }
    }
}