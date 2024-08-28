using Microsoft.VisualBasic.ApplicationServices;
using MyApp.Data.Models.EntityModel;
using ProductManagmentUserPanel.Helpers;
using System.Windows;
using System.Windows.Input;
using MyApp.Data.Models;
using ProductManagmentUserPanel.Services;
using ProductManagmentUserPanel.Views;

namespace ProductManagmentUserPanel.ViewModels
{
	class AuthorizationVM:BaseVM
    {


		public RelayCommand OpenPopupCommand { get; set; }
		public RelayCommand VerifyCodeCommand { get; set; }
		public RelayCommand CloseVerificationPopupCommand { get; set; }

		private string _email;
		private string _firstName;
		private string _lastName;
		private string _password;
		private DateOnly _selectedDate;

		
		private string verificationCode;
		private int randomCode;

		

		public string VerificationCode
		{
			get => verificationCode;
			set
			{
				verificationCode=value;
				OnPropertyChanged(nameof(VerificationCode));
			}
		}
		private bool _isPopupOpen;
		public bool IsPopupOpen
		{
			get { return _isPopupOpen; }
			set
			{
				_isPopupOpen = value;
				OnPropertyChanged(nameof(IsPopupOpen));
			}
		}
		private MyApp.Data.Models.EntityModel.User _userData;
		private int yourRandomCode;

		public MyApp.Data.Models.EntityModel.User UserData
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



        public AuthorizationVM()
        {
			LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
			SignUpCommand = new RelayCommand(ExecuteSignUp, CanExecuteSignUp);
			VerifyCodeCommand=new RelayCommand(ExecuteVerifyCode);
			CloseVerificationPopupCommand=new RelayCommand(ExecuteCloseVerificationPopup);
			
		}


		private void ExecuteCloseVerificationPopup(object parameter)
		{
			IsPopupOpen=false;
			VerificationCode=string.Empty;
		}

		private void ExecuteVerifyCode(object parameter)
		{

			var param = parameter as AuthorizationVM;


			if (param is not null)
			{

				var userData = param.UserData;
				var randomCode = param.VerificationCode;


				if (randomCode.ToString() != RandomCode.ToString())
				{

					MessageBox.Show("Girilen kod yanlış. Lütfen tekrar deneyin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				else
				{
					myAppDbContext.Users.Add(userData);
					myAppDbContext.SaveChanges();
					MessageBox.Show("Kod doğru!", "Başarı", MessageBoxButton.OK, MessageBoxImage.Information);
					

					
				}
			}
			else
			{

				MessageBox.Show("Parametreler geçersiz. Lütfen tekrar deneyin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


		private bool CanExecuteLogin(object parameter)
		{

			return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
		}

		private void ExecuteLogin(object parameter)
		{
			var passwordManager = new PasswordManager();
			UserData = myAppDbContext.Users.FirstOrDefault(e => e.Email == Email);

			if (UserData is not null && passwordManager.VerifyPassword(UserData.Password, Password))
			{
				
				var mainUser = new MainUserView();
				var mainUserVM = new MainUserVM(App.Container.GetInstance<INavigationService>());
				mainUserVM.User = UserData;
				mainUser.DataContext = mainUserVM;

				
				Application.Current.MainWindow.Hide();

				
				mainUser.Show();
				
			}
			else
			{
				UserData = null;
				MessageBox.Show("Cannot find user or password is incorrect.");
			}
		}


		private bool CanExecuteSignUp(object parameter)
		{

			return !string.IsNullOrEmpty(FirstName) &&
				   !string.IsNullOrEmpty(LastName) &&
				   !string.IsNullOrEmpty(Email) &&
				   !string.IsNullOrEmpty(Password);
		}

		private void ExecuteSignUp(object parameter)
		{
			UserData =myAppDbContext.Users.FirstOrDefault(e=>e.Email==Email);
			if (UserData is not null)
			{
				MessageBox.Show("This user is exist");
				UserData=null;
			}
			else
			{
				var emailService = new MailService();
				Random random = new Random();

				RandomCode = random.Next(100, 1000);
				PasswordManager password = new();
				emailService.SendMail(Email, "Welcome to Our Service", $"{RandomCode}");
				UserData=new() { Firstname=FirstName, Lastname=LastName, Email=Email, DateofBirth =SelectedDate, Password=password.HashPassword(Password) };
				IsPopupOpen=true;



			};


		}
	}
}
