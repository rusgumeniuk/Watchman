using System.Collections.ObjectModel;

using Watchman.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchmenPage : ContentPage
    {
        public WatchmenPage()
        {
            InitializeComponent();
            this.BindingContext = new WatchmenViewModel()
            {
                Watchmen = new ObservableCollection<PersonalInfo>()
                {
                    new PersonalInfo()
                    {
                        FirstName = "Alan",
                        SecondName = "Moore"
                    }
                }
            };
        }
    }
}