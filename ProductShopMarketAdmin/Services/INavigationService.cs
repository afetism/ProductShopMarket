using ProductShopMarketAdmin.ViewModels;
using System.Windows.Controls;

namespace ProductShopMarketAdmin.Services;
public interface INavigationService
 {
	void Navigate<TView, TViewModel>() where TView : Page where TViewModel : BaseViewModel;

}

