using System;
using System.Collections.ObjectModel;
using Watchman.BusinessLogic.Models.Users;
using Watchman.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WatchmanPage : ContentPage
    {
        public WatchmanPage()
        {
            InitializeComponent();
            BindingContext = new WatchmanViewModel()
            {
                Patients = new ObservableCollection<PersonalInfo>()
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

    class PatientProfile : Patient<Guid>
    {
        

    }
}
