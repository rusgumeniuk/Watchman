using System;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Watchman.BusinessLogic.Services;
using Watchman.Mobile.Views;
using Xamarin.Forms;

namespace Watchman.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private ITokenService _tokenService;
        private string _email;
        private string _password;
        private string _errorMessage;

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Error
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public bool IsValid
        {
            get
            {
                try
                {
                    return !string.IsNullOrEmpty(Password) &&
                           !string.IsNullOrWhiteSpace(Email) &&
                           new MailAddress(Email) != null;
                }
                catch (FormatException)
                {
                    return false;
                }
            }
        }

        public INavigation Navigation { get; set; }

        public ICommand SubmitCommand { protected set; get; }


        public LoginViewModel(INavigation navigation, ITokenService tokenService)
        {
            Navigation = navigation;
            _tokenService = tokenService;
            SubmitCommand = new Command(async () => await LogIn());
        }

        private async Task LogIn()
        {
            if (IsValid)
            {
                try
                {
                    Application.Current.MainPage = new MainPage();
                    //var token = await _tokenService.GetTokenAsync(Email, Password);
                    //if (!string.IsNullOrEmpty(token))
                    //    await Navigation.PushAsync(new MainPage());
                    //else
                    //    Error = "Invalid credentials";
                }
                catch (WebException ex)
                {
                    Error = ex.Message;
                }
                
            }
        }
    }
}
