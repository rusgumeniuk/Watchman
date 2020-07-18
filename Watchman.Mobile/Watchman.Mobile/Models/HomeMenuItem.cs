namespace Watchman.Mobile.Models
{
    public enum MenuItemType
    {
        Browse,
        Home,
        Account,
        Patient,
        Watchman,
        PatientGeneral,
        PatientMeasurement,
        PatientWatchmen,
        About,
        Login,
        Registration
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
