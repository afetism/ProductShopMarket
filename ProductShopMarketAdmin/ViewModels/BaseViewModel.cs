using MyApp.Data.Data;
using System.Collections;
using System.ComponentModel;

namespace ProductShopMarketAdmin.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
	protected readonly MyAppDbContext myAppDbContext=new MyAppDbContext();

	public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	Dictionary<String, List<string>> Errors = new();
	public bool HasErrors => Errors.Count > 0;

	public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

	public IEnumerable GetErrors(string? propertyName)
	{
		if (Errors.ContainsKey(propertyName))
		{
			return Errors[propertyName];

		}
		else
		{
			return Enumerable.Empty<string>();
		}

	}
}
