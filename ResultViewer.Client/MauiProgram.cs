using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using ResultViewer.Client.Core.Constants;
using ResultViewer.Client.Models;
using ResultViewer.Client.Platforms.Windows;
using ResultViewer.Client.ViewModels;
using ResultViewer.Client.Services;

namespace ResultViewer.Client;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        // Create a MauiApp builder instance
        var builder = MauiApp.CreateBuilder();

        // Configure the MauiApp builder:
        builder
            .UseMauiApp<App>() // Specify the main Maui application class
            .UseMauiCommunityToolkit() // Use the Maui Community Toolkit for additional functionality
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); // Configure fonts for the app
            });
        builder.ConfigureLifecycleEvents(lifecycle =>
        {
#if WINDOWS
            // Platform-specific code for Windows
            lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
                del.ExtendsContentIntoTitleBar = false;
            }));
#endif
        });

#if WINDOWS
        // Register a Windows-specific service
        builder.Services.AddSingleton<ITrayService, TrayService>();
#endif
        // Register a service for handling file saving
        builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

        // Register a GraphQL client and configure its base address
        builder.Services.AddRVClient().ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:7241/graphql"));

        // Register services and view models
        builder.Services.AddScoped<IReportModel, ReportModel>();
        builder.Services.AddScoped<IReportViewModel, ReportViewModel>();
        builder.Services.AddScoped<IAnalysisModel, AnalysisModel>();
        builder.Services.AddScoped<IAnalysisViewModel, AnalysisViewModel>();
        builder.Services.AddScoped<RangeViewModel>();

        // Load configuration from a JSON file
        builder.Configuration.AddJsonFile("wwwroot\\constants.json");

        // Retrieve the configuration values
        var config = builder.Configuration.Get<ConstantsBase>();

        // Register the configuration with Dependency Injection (DI)
        builder.Services.AddSingleton(config);

        // Add Maui Blazor WebView support
        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        // Add Blazor WebView developer tools and logging in DEBUG mode
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
		return builder.Build();
	}
}
