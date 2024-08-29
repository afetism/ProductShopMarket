using MyApp.Data.Models.EntityModel;
using ProductManagmentUserPanel.Helpers;
using ProductManagmentUserPanel.Views;
using System.Collections.ObjectModel;

namespace ProductManagmentUserPanel.ViewModels;

public class CartVM:BaseVM
{
	private ObservableCollection<Product> products=[];
	

	public ObservableCollection<Product> Products
	{
		get => products;
		set
		{
			products=value;
			OnPropertyChanged(nameof(Products));	
		}
	}
	


	public RelayCommand ButtonBack { get; set; }
    public CartVM()
    {
		ButtonBack=new RelayCommand(executeBack);

	}

	private void executeBack(object o)
	{
		
			var dc = App.Container.GetInstance<MainUserVM>();
			dc.CurrentPage = (App.Container.GetInstance<ProductListView>())!;
			dc.CurrentPage.DataContext = App.Container.GetInstance<ProductListVM>();
		
	}
}
