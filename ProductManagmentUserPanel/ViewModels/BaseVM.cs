using MyApp.Data.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagmentUserPanel.ViewModels
{
   public  class BaseVM:INotifyPropertyChanged
    {
		protected readonly MyAppDbContext myAppDbContext = new MyAppDbContext();

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		public event PropertyChangedEventHandler? PropertyChanged;
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
}
