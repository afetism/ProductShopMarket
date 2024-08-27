using MyApp.Data.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProductManagmentUserPanel.ViewModels
{
    class AuthorizationVM:BaseVM
    {
		private string _email;
		private string _firstName;
		private string _lastName;
		private string _password;
		private DateOnly _selectedDate;

		private bool _isVerificationPopupOpen;
		private string verificationCode;
		private int randomCode;

		public bool IsVerificationPopupOpen
		{
			get => _isVerificationPopupOpen;
			set
			{
				if (_isVerificationPopupOpen != value)
				{
					_isVerificationPopupOpen = value;
					OnPropertyChanged(nameof(IsVerificationPopupOpen));
				}
			}
		}
		public string VerificationCode
		{
			get => verificationCode;
			set
			{
				verificationCode=value;
				OnPropertyChanged(nameof(VerificationCode));
			}
		}
		private User _userData;
		private int yourRandomCode;

		public User UserData
		{
			get => _userData;
			set
			{
				_userData = value;
				OnPropertyChanged(nameof(UserData));
			}
		}

		public string Email
		{
			get => _email;
			set
			{
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}

		public string FirstName
		{
			get => _firstName;
			set
			{
				_firstName = value;
				OnPropertyChanged(nameof(FirstName));
			}
		}

		public string LastName
		{
			get => _lastName;
			set
			{
				_lastName = value;
				OnPropertyChanged(nameof(LastName));
			}
		}

		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
				CommandManager.InvalidateRequerySuggested();
			}
		}

		public DateOnly SelectedDate
		{
			get => _selectedDate;
			set
			{
				_selectedDate = value;
				OnPropertyChanged(nameof(SelectedDate));
			}
		}
		public int RandomCode
		{
			get => randomCode;
			set
			{
				randomCode=value;
				OnPropertyChanged(nameof(RandomCode));
			}
		}


		public ICommand LoginCommand { get; }
		public ICommand SignUpCommand { get; }

		
	}
}
