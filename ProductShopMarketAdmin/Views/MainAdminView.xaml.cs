using ProductShopMarketAdmin.Services;
using ProductShopMarketAdmin.ViewModels;
using System.Windows;

namespace ProductShopMarketAdmin.Views;


public partial class MainAdminView : Window
{
	public MainAdminView()
	{
		InitializeComponent();
		this.DataContext = new MainAdminVM(App.Container.GetInstance<INavigationService>());
	}

	private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if(e.ChangedButton==System.Windows.Input.MouseButton.Left)
		{
			this.DragMove();
		}
	}
	private bool IsMaximized = false;
	private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if(e.ClickCount==2)
		{
			if(IsMaximized)
			{
				this.WindowState=WindowState.Normal;
				this.Width=1080;
				this.Height=720;

				IsMaximized = false;
			}
			else
			{
				this.WindowState = WindowState.Maximized;
				IsMaximized=true ;
			}
		}
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{
		Environment.Exit(0);
    }
}
