namespace Watchman.Mobile.Infrastructure
{
    public class UserManager
    {
        private static UserManager _instance;

        public static UserManager Instance => _instance == null ? (_instance = new UserManager()) : _instance;

        internal bool IsLoggedIn()
        {
            if (Xamarin.Essentials.Preferences.ContainsKey("APP_TOKEN"))
            {
                var token = Xamarin.Essentials.Preferences.Get("APP_TOKEN", null);
                return CheckTokenValid(token);
            }

            return false;
        }

        private bool CheckTokenValid(string token)
        {
            return true;
        }
    }
}
