using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Watchman.Mobile.Pages;

using Xamarin.Forms;

namespace Watchman.Mobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _email;
        private string _password;

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


        public LoginViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SubmitCommand = new Command(LogIn);
        }

        private void LogIn()
        {
            if (IsValid)
                Navigation.PushAsync(new MainPage());
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
