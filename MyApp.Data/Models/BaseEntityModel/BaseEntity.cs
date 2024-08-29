using System.ComponentModel;

namespace MyApp.Data.Models.BaseEntityModel;

public class BaseEntity:INotifyPropertyChanged
{
    public int Id { get; set; }
	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}


	public event PropertyChangedEventHandler? PropertyChanged;
}
