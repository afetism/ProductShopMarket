using MyApp.Data.Models.EntityModel;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Media;
using System.Xml.Linq;
using Color = System.Windows.Media.Color;

namespace ProductShopMarketAdmin.ViewModels;

public class AllUserVM:BaseViewModel
{
	public System.Windows.Media.Brush BgColor
	{
		get => bgColor;
		set
		{
			bgColor=value;
			OnPropertyChanged(nameof(BgColor));
		}
	}
	public char Character
	{
		get => character; set
		{
			character=value;
			OnPropertyChanged($"{nameof(Character)}");
		}
	}
	private ObservableCollection<User> allusers;

	public ObservableCollection<User> AllUsers
	{
		get => allusers;
		set
		{
			allusers=value;
			OnPropertyChanged(nameof(AllUsers));
		}
	}

	private ObservableCollection<User> filterUser;
	private int countUser;
	private string searchText;
	private System.Windows.Media.Brush bgColor;
	private char character;

	public ObservableCollection<User> FilterUser
	{
		get => filterUser;
		set
		{
			filterUser=value;
			OnPropertyChanged(nameof(FilterUser));
		}
	}

	public int CountUser { 
		get => countUser;
		set
		{
			countUser=value;
			OnPropertyChanged(nameof(CountUser));	
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

	public AllUserVM()
    {
		Load();

	}
	void LoadCol()
	{
		var random = new Random();
		BgColor = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
	}

	public void Load()
	{
		
		if (string.IsNullOrEmpty(SearchText))
		{
			FilterUser=new ObservableCollection<User>([.. myAppDbContext.Users]);
			CountUser= myAppDbContext.Users.Count();
			
		}
		else
		{
			FilterUser=new ObservableCollection<User>(myAppDbContext.Users.ToList().Where(m => m.Firstname.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
			CountUser=myAppDbContext.Users.ToList().Where(m => m.Firstname.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).Count();

		}
		

	}

}
