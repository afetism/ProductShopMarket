using ProductShopMarketAdmin.Command;
using ProductShopMarketAdmin.Services;
using ProductShopMarketAdmin.Views;
using System.Windows;
using System.Windows.Controls;

namespace ProductShopMarketAdmin.ViewModels;

public class MainAdminVM:BaseViewModel
{

	private Page currentPage;
	public Page CurrentPage
	{
		get => currentPage;
		set
		{
			currentPage=value;
			OnPropertyChanged(nameof(CurrentPage));
		}
	}

	private readonly INavigationService navigationService;


	public RelayCommand CategoryCommand { get; set; }
    public RelayCommand DashboardCommand { get; set; }
    public RelayCommand ProductCommand { get; set; }
    public RelayCommand AllUserCommand { get; set; }

    public MainAdminVM(INavigationService navigationService)
    {
        CategoryCommand=new(executeCategories);
        DashboardCommand=new(executeDashboard);
		ProductCommand=new(executeProducts);
		AllUserCommand=new(executeAllUser);
		this.navigationService=navigationService;
	}

	private void executeAllUser(object obj)
	{
		navigationService.Navigate<AllUserView, AllUserVM>();
	}

	private void executeProducts(object obj)
	{
		navigationService.Navigate<ProductView, ProductVM>();
	}

	private void executeDashboard(object obj)
	{
		navigationService.Navigate<DashboardView, DashboardVM>();
	}

	private void executeCategories(object obj)
	{
		navigationService.Navigate<CategoryView, CategoryVM>();
	}
}
