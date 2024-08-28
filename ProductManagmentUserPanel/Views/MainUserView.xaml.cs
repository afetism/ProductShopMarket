using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductManagmentUserPanel.Views
{
    /// <summary>
    /// Interaction logic for MainUserView.xaml
    /// </summary>
    public partial class MainUserView : Window
    {
        public MainUserView()
        {
            InitializeComponent();
        }

		private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton==System.Windows.Input.MouseButton.Left)
			{
				this.DragMove();
			}
		}
		private bool IsMaximized = false;
		private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ClickCount==2)
			{
				if (IsMaximized)
				{
					this.WindowState=WindowState.Normal;
					this.Width=1080;
					this.Height=720;

					IsMaximized = false;
				}
				else
				{
					this.WindowState = WindowState.Maximized;
					IsMaximized=true;
				}
			}
		}
	}
}
