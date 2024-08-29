using MyApp.Data.Models.EntityModel;
using ProductManagmentUserPanel.Helpers;
using ProductManagmentUserPanel.Services;
using ProductManagmentUserPanel.Views;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ProductManagmentUserPanel.ViewModels;

class MainUserVM:BaseVM
{

	private Page currentPage;
	public Page CurrentPage {
		get => currentPage;
	    set 
		{ 
	   
	       currentPage=value; 
	       OnPropertyChanged(nameof(CurrentPage));
	       } 
    
	}
	private User user;
	public User User
	{

		get => user;
		set
		{
			user=value;
			OnPropertyChanged(nameof(User));
		}
	}

	private Category? category;
	public Category? Category { 
		get => category;
		set
		{
			category=value;
			OnPropertyChanged(nameof(Category));
		}
	}

	private ObservableCollection<Category> categories;
	public ObservableCollection<Category> Categories 
	{ 
		get => categories;
		set
		{
			categories=value;
			OnPropertyChanged(nameof(Categories));
		}

	}

	private ObservableCollection<Product> products;
	public ObservableCollection<Product> Products 
	{ 
		get => products;
		set
		{
			products=value;
			OnPropertyChanged(nameof(Products));
		}
	}

	private ObservableCollection<string> rangePrices;
	public ObservableCollection<string> RangePrices
	{
		get => rangePrices;
		set
		{
			rangePrices=value;
			OnPropertyChanged(nameof(RangePrices));
		}
	}
	private string rangePrice;
	public string RangePrice {
		get => rangePrice;
		set
		{
			if (rangePrice != value)
			{
				rangePrice = value;
				OnPropertyChanged(nameof(RangePrice));
				UpdateMinMaxPrices(rangePrice);
				OnPropertyChanged(nameof(IsRangeSelected)); 
			}
		}
	}
	private int minPrice;
	public int MinPrice
	{
		get => minPrice;
		set
		{ 
			minPrice=value;
			OnPropertyChanged(nameof(MinPrice));
		}
	}
	private int maxPrice;
	public int MaxPrice
	{
		get => maxPrice;
		set
		{
			maxPrice=value;
			OnPropertyChanged(nameof(MaxPrice));
		}
	}


	private bool isOpenPopupMenu;
	
	

	public bool IsOpenPopupMenu
	{
		get => isOpenPopupMenu;
		set
		{
			isOpenPopupMenu=value;
			OnPropertyChanged(nameof(IsOpenPopupMenu));
		}
	}




	private bool isCheck;
	public bool IsCheck
	{
		get => isCheck;
		set
		{
			isCheck=value;
			OnPropertyChanged(nameof(IsCheck));
			if (isCheck) 
			{
				ClearFiltersCommand.Execute(null);
			}
		}
	}


	private readonly INavigationService navigationService;

    public RelayCommand OpenPopupMenu { get; set; }
    public RelayCommand ShowList { get; set; }
	public RelayCommand ClosePopupMenu { get; set; }
    public RelayCommand ClearFiltersCommand { get; set; }
    public  RelayCommand ShowCart { get; set; }

    public bool IsRangeSelected => !string.IsNullOrEmpty(RangePrice);


	public MainUserVM(INavigationService navigationService)
    {
            this.navigationService=navigationService;
		    LoadProducts();
		    OpenPopupMenu=new(executeOpenPopUp);
		    ShowList=new(executeShowList);
		    ClosePopupMenu=new(executeClosePopupMenu);
		    ClearFiltersCommand=new(executeClearFilters);
		ShowCart=new(executeCart);
	}

	private void executeCart(object o)
	{
		CurrentPage = (App.Container.GetInstance<CartView>())!;
		CurrentPage.DataContext = App.Container.GetInstance<CartVM>();
	}

	private void executeClearFilters(object o)
	{
		Category=null;
		RangePrice=string.Empty;
		MaxPrice=0;
		MinPrice=0;
		OnPropertyChanged(nameof(Category));
		OnPropertyChanged(nameof(RangePrice));
		OnPropertyChanged(nameof(MinPrice));
		OnPropertyChanged(nameof(MaxPrice));
		IsCheck=false;
	}

	private void executeClosePopupMenu(object o)
	{
		IsOpenPopupMenu=false;
		LoadProducts();
	}

	private void executeShowList(object o)
	{
		
	}

	private void executeOpenPopUp(object o)
	{
		Categories=new ObservableCollection<Category>(myAppDbContext.Categories);
		RangePrices=new ObservableCollection<string> { "0-50", "50-100", "100-200", "300-400", "400-500", "500-600", "700-800", "900-1000","more than 1000" };

		IsOpenPopupMenu=true;


	}
	private void UpdateMinMaxPrices(string rangePrice)
	{
		if (string.IsNullOrWhiteSpace(rangePrice))
		{
			MinPrice = 0;
			MaxPrice = 0;
			return;
		}

		var parts = rangePrice.Split('-');

		if (parts.Length == 2 && int.TryParse(parts[0], out int min) && int.TryParse(parts[1], out int max))
		{
			MinPrice = min;
			MaxPrice = max;
		}
		else if (rangePrice.Contains("more than") && int.TryParse(rangePrice.Replace("more than", "").Trim(), out int minOnly))
		{
			MinPrice = minOnly;
			MaxPrice = int.MaxValue;
		}
		else
		{
			MinPrice = 0;
			MaxPrice = 0;
		}
	}



	public void LoadProducts()
	{

		CurrentPage = (App.Container.GetInstance<ProductListView>())!;
		CurrentPage.DataContext = App.Container.GetInstance<ProductListVM>();
		var dc= CurrentPage.DataContext as ProductListVM;
		if (dc is not null && Category is not  null || MinPrice!=0 || MaxPrice!=0)
		{
			dc!.FilterItem=new() { Category=Category, MinPrice=MinPrice, MaxPrice=MaxPrice };
			dc!.Load();
		}
	}
}


