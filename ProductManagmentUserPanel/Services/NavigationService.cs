using ProductManagmentUserPanel;
using ProductManagmentUserPanel.ViewModels;
using System.Windows.Controls;

namespace ProductManagmentUserPanel.Services;
class NavigationService : INavigationService
{
	public void Navigate<TView, TViewModel>() where TView : Page where TViewModel : BaseVM
	{
		var mainVm = App.Current.MainWindow.DataContext as MainUserVM;
		if(mainVm is not  null )
		{
			mainVm.CurrentPage = (App.Container.GetInstance<TView>())!;
			mainVm.CurrentPage.DataContext = App.Container.GetInstance<TViewModel>();
		}
	}
}

