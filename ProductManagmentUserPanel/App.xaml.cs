using ProductManagmentUserPanel.Services;
using ProductManagmentUserPanel.ViewModels;
using ProductManagmentUserPanel.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ProductManagmentUserPanel;

public partial class App : Application
{
	public static SimpleInjector.Container Container { get; set; } = new();
	public App()
	{
		View();
		ViewModel();
		AddOtherServices();
	}


	private void AddOtherServices()
	{
		Container.RegisterSingleton<INavigationService, NavigationService>();
	}

	private void ViewModel()
	{
		Container.RegisterSingleton<ProductListVM>();
		Container.RegisterSingleton<CartVM>();
		Container.RegisterSingleton<MainUserVM>();
	}

	private void View()
	{
		Container.RegisterSingleton<ProductListView>();
		Container.RegisterSingleton<CartView>();
		Container.RegisterSingleton<MainUserView>();
	}
	
}
