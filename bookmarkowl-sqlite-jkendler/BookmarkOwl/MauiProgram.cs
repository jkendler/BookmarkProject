using BookmarkOwl.Core;
using BookmarkOwl.Core.Interfaces;
using BookmarkOwl.Core.Services;
using BookmarkOwl.Core.ViewModels;
using BookmarkOwl.Pages;
using BookmarkOwl.Services;
using CommunityToolkit.Maui;

namespace BookmarkOwl;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkit();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<SettingsPage>();
		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<LinkListViewModel>();
		builder.Services.AddSingleton<SettingsViewModel>();
		builder.Services.AddSingleton<IPreferencesService, PreferencesService>();
		builder.Services.AddSingleton<IAlertService, AlertService>();
		builder.Services.AddSingleton<IBrowserService, BrowserService>();

		// Singleton ist in der kompletten Anwendung verfügbar
		//builder.Services.AddSingleton<IRepository>(repository => new SqliteRepository(dbPath: FileSystem.AppDataDirectory));

		string apiUrl = Preferences.Get("ApiUrl", string.Empty); 

		string apiToken = Preferences.Get("ApiToken", string.Empty);

		builder.Services.AddSingleton<IRepository>(repository => new RestRepository(apiUrl, apiToken));


		return builder.Build();
	}
}
