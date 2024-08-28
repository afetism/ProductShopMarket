using System.Windows.Controls;
using ProductManagmentUserPanel.ViewModels;
namespace ProductManagmentUserPanel.Services;
public interface INavigationService
 {
	void Navigate<TView, TViewModel>() where TView : Page where TViewModel : BaseVM;

}

