using Watchman.Mobile.Infrastructure;
using Watchman.Mobile.Views;
using Xamarin.Forms;
using MainPage = Watchman.Mobile.Views.MainPage;

namespace Watchman.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (!UserManager.Instance.IsLoggedIn())
            {
                MainPage = new LoginPage();
                return;
            }

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
