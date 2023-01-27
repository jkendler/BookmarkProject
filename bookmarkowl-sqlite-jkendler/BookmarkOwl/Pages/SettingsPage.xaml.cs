using BookmarkOwl.Core.ViewModels;

namespace BookmarkOwl.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}
