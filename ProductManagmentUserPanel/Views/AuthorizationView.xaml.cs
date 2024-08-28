using ProductManagmentUserPanel.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ProductManagmentUserPanel.Views;


public partial class AuthorizationView : Window
    {
        public AuthorizationView()
        {
            InitializeComponent();
            this.DataContext = new AuthorizationVM();
        }

	private void SwitchToSignUp(object sender, MouseButtonEventArgs e)
	{
		LoginPanel.Visibility = Visibility.Collapsed;
		SignUpPanel.Visibility = Visibility.Visible;
	}

	private void SwitchToLogin(object sender, MouseButtonEventArgs e)
	{
		SignUpPanel.Visibility = Visibility.Collapsed;
		LoginPanel.Visibility = Visibility.Visible;
	}

	private void CloseApp_Click(object sender, RoutedEventArgs e)
	{
		Environment.Exit(0);
	}
}
