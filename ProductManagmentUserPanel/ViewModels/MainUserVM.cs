using Microsoft.EntityFrameworkCore.Metadata;
using MyApp.Data.Models.EntityModel;
using ProductManagmentUserPanel.Services;
using ProductManagmentUserPanel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ProductManagmentUserPanel.ViewModels
{
    class MainUserVM:BaseVM
    {

		private Page currentPage;
		public Page CurrentPage { get => currentPage;
			                      set { 
			                        
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
				OnPropertyChanged(nameof(User)) ;
			}
		}
		private readonly INavigationService navigationService;
		public MainUserVM(INavigationService navigationService)
        {
            this.navigationService=navigationService;
			LoadProducts();


		}
		public void LoadProducts()
		{


			
			CurrentPage = (App.Container.GetInstance<ProductListView>())!;
			CurrentPage.DataContext = App.Container.GetInstance<ProductListVM>();
		}
	}

	


}
