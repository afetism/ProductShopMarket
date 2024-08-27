using ProductShopMarketAdmin.Services;
using ProductShopMarketAdmin.ViewModels;
using ProductShopMarketAdmin.Views;
using System.ComponentModel;
using System.Windows;

namespace ProductShopMarketAdmin;


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

		Container.RegisterSingleton<CategoryVM>();
		Container.RegisterSingleton<ProductVM>();
		Container.RegisterSingleton<AllUserVM>();
		Container.RegisterSingleton<DashboardVM>();

	}

	private void View()
	{

		Container.RegisterSingleton<CategoryView>();
		Container.RegisterSingleton<ProductView>();
		Container.RegisterSingleton<AllUserView>();
		Container.RegisterSingleton<DashboardView>();
	}


}
