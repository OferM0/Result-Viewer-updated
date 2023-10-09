using ChartJs.Blazor.BarChart;
using ChartJs.Blazor.Common.Axes.Ticks;
using ChartJs.Blazor.Common;
using Microsoft.AspNetCore.Components;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.Util;
using ChartJs.Blazor.Common.Axes;

namespace ResultViewer.Client.Core.Components.Charts.BarCharts
{
    public partial class BarChart
    {
        private BarConfig _config; // Configuration for the bar chart

        [Parameter]
        public List<string> Lables { get; set; } = new List<string>(); // Labels for the chart data

        [Parameter]
        public List<double> MinValues { get; set; } = new List<double>(); // Minimum values for chart data

        [Parameter]
        public List<double> MaxValues { get; set; } = new List<double>(); // Maximum values for chart data

        [Parameter]
        public List<double> AvgValues { get; set; } = new List<double>(); // Average values for chart data

        [Parameter]
        public string Title { get; set; } = ""; // Title for the bar chart


        // Array of color strings for chart data elements
        private String[] colorArray = {           
            ColorUtil.ColorHexString(237, 125, 49),
            ColorUtil.ColorHexString(91, 155, 213),
            ColorUtil.ColorHexString(247, 181, 56),
            ColorUtil.ColorHexString(246, 195, 179),
            ColorUtil.ColorHexString(255, 231, 225),
            ColorUtil.ColorHexString(216, 87, 42),
            ColorUtil.ColorHexString(219, 124, 26),
            ColorUtil.ColorHexString(128, 0, 128),
            ColorUtil.ColorHexString(0, 0, 255),
            ColorUtil.ColorHexString(0, 128, 0),
            ColorUtil.ColorHexString(255, 0, 0),
            ColorUtil.ColorHexString(255, 255, 0),
            ColorUtil.ColorHexString(0, 255, 0),
            ColorUtil.ColorHexString(255, 0, 255),
            ColorUtil.ColorHexString(0, 255, 255),
            ColorUtil.ColorHexString(192, 192, 192),
            ColorUtil.ColorHexString(128, 128, 128),
            ColorUtil.ColorHexString(128, 0, 0),
            ColorUtil.ColorHexString(128, 128, 0),
            ColorUtil.ColorHexString(0, 128, 0),
            ColorUtil.ColorHexString(128, 0, 128),
            ColorUtil.ColorHexString(0, 128, 128),
            ColorUtil.ColorHexString(0, 0, 128),
            ColorUtil.ColorHexString(139, 69, 19),
            ColorUtil.ColorHexString(0, 100, 0),
            ColorUtil.ColorHexString(128, 128, 0),
            ColorUtil.ColorHexString(0, 128, 128),
            ColorUtil.ColorHexString(128, 0, 128),
            ColorUtil.ColorHexString(128, 128, 128),
            ColorUtil.ColorHexString(255, 0, 0),
            ColorUtil.ColorHexString(255, 255, 0),
            ColorUtil.ColorHexString(0, 255, 0),
            ColorUtil.ColorHexString(0, 255, 255),
            ColorUtil.ColorHexString(0, 0, 255),
            ColorUtil.ColorHexString(255, 0, 255),
            ColorUtil.ColorHexString(192, 192, 192),
            ColorUtil.ColorHexString(128, 128, 128),
            ColorUtil.ColorHexString(128, 0, 0),
            ColorUtil.ColorHexString(128, 128, 0),
            ColorUtil.ColorHexString(0, 128, 0),
            ColorUtil.ColorHexString(128, 0, 128),
            ColorUtil.ColorHexString(0, 128, 128),
            ColorUtil.ColorHexString(0, 0, 128)
        };

        // Example labels for chart data
        private List<string> exampleLables = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41" };

        // Initialize the component asynchronously
        protected override async Task OnInitializedAsync()
        {
            _config = new BarConfig
            {
                Options = new BarOptions
                {
                    Responsive = true,
                    MaintainAspectRatio = false,
                    Legend = new Legend
                    {
                        Display = false
                    },
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = Title
                    },
                    ResponsiveAnimationDuration = 2,
                    Scales = new BarScales
                    {
                        YAxes = new[]
                        {
                            new LinearCartesianAxis
                            {
                                Ticks = new LinearCartesianTicks
                                {
                                    BeginAtZero = true
                                }
                            }
                        }
                    }
                }
            };

            // Add labels for Min, Max, and Avg to the chart configuration
            foreach (var label in new List<string> { "Min", "Max", "Avg" })
            {
                _config.Data.Labels.Add(label);
            }

            // Iterate through the chart data and create datasets for each
            for (int i = 0; i < Lables.Count; i++)
            {
                List<double> MixedValues = new List<double> { MinValues.ElementAt(i), MaxValues.ElementAt(i), AvgValues.ElementAt(i) };

                // Create a dataset for the chart data with specified properties
                BarDataset<double> dataset = new BarDataset<double>(MixedValues)
                {
                    Label = Lables.ElementAt(i),
                    BorderWidth = 3,
                    BackgroundColor = colorArray[i]
                };

                // Add the dataset to the chart configuration
                _config.Data.Datasets.Add(dataset);
            }
        }
    }
}