using System.Net.Http;
using Watchman.Mobile.Services;
using Watchman.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel(
                this.Navigation, new TokenService(new WatchmanHttpClient(new HttpClient()))
                ){Email = "rus@gmail.com", Password = "password"};
        }
    }
}