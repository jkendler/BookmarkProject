using BookmarkOwl.Core.ViewModels;

namespace BookmarkOwl;

public partial class MainPage : ContentPage
{
	

	public MainPage(LinkListViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}

	
}

