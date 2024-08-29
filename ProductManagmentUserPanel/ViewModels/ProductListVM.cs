using MyApp.Data.Models.EntityModel;
using ProductManagmentUserPanel.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProductManagmentUserPanel.ViewModels;

class ProductListVM:BaseVM
 {
	private ObservableCollection<Product> _products;
	private string searchText;

	public ObservableCollection<Product> Products
	{
		get { return _products; }
		set
		{
			_products = value;
			OnPropertyChanged(nameof(Products));
		}
	}

   
    public FilterItem FilterItem { get; set; }
    public RelayCommand AddCartList { get; set; }
	public RelayCommand OpenPopupMenu { get; set; }

	public ProductListVM()
	{
		Load();
		AddCartList=new(executeCart);


	}

	private void executeCart(object o)
	{
		var product = o as Product;
		var dc = App.Container.GetInstance<CartVM>();
		var existProduct = dc.Products.FirstOrDefault(e => e.Id == product.Id);

		if (existProduct is null)
		{
			product.CurrentCount = 1;  // Initialize CurrentCount for a new product
			dc.Products.Add(product);
		}
		else
		{
			existProduct.CurrentCount++;  // Increment CurrentCount for an existing product
		}


	}

	public string SearchText { 
		get => searchText;
		set
		{
			searchText=value;
			OnPropertyChanged(nameof(SearchText));
			Load();
		}
	}

	public void Load ()
	{
		var query = myAppDbContext.Products.AsQueryable();
		if (FilterItem is not null)

		{ 
			if (FilterItem.Category is not null)
			{
				query =query.Where(q => q.CategoryId==FilterItem.Category.Id);
			}
			else if(FilterItem.MinPrice>0)
			{
				query=query.Where(q => q.Price>FilterItem.MinPrice);
			}
			else if(FilterItem.MaxPrice>0)
			{
				query=query.Where(q => q.Price<=FilterItem.MaxPrice);
			}
			
		}
		if (string.IsNullOrEmpty(SearchText))
		{
			Products = new ObservableCollection<Product>(query.ToList());
		}
		else
		{
			Products=new ObservableCollection<Product>(query.ToList().Where(m => m.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
			

		}
	}
}
