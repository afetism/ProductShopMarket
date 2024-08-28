using MyApp.Data.Models.EntityModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProductManagmentUserPanel.ViewModels;

class ProductListVM:BaseVM
 {
	private ObservableCollection<Product> _products;
	public ObservableCollection<Product> Products
	{
		get { return _products; }
		set
		{
			_products = value;
			OnPropertyChanged(nameof(Products));
		}
	}
	public ICommand AddCartList { get; set; }
	public ProductListVM()
	{
		Load();
		
	}

	public void Load ()
	{
		Products = new ObservableCollection<Product>(myAppDbContext.Products.ToList());
	}
}
