using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Watchman.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public ListView ListView;
        public MenuPage()
        {
            InitializeComponent();

            BindingContext = new MenuPageViewModel();
            ListView = MenuItemsListView;
        }

        class MenuPageViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMasterMenuItem> MenuItems { get; set; }

            public MenuPageViewModel()
            {
                MenuItems = new ObservableCollection<MainMasterMenuItem>(new[]
                {
                    new MainMasterMenuItem { Id = 0, Title = "Patient", TargetType = typeof(PatientPage) },
                    new MainMasterMenuItem { Id = 0, Title = "Watchman", TargetType = typeof(WatchmanPage) },
                    new MainMasterMenuItem { Id = 0, Title = "Account", TargetType = typeof(AccountPage) },
                    new MainMasterMenuItem { Id = 0, Title = "About", TargetType = typeof(AboutPage) }
                });
            }

            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}