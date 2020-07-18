using System;
using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.BusinessLogic.Models.Users;
using Watchman.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralInfoForWatchmanPage : ContentPage
    {
        public GeneralInfoForWatchmanPage()
        {
            InitializeComponent();
            BindingContext = new GeneralInfoForWatchmanViewModel()
            {
                ActivityState = new SportActivityState(),
                HealthState = new NormalHealthState(),
                PatientPersonalInformation = new PersonalInfo()
                {
                    BirthDay = new DateTime(1953, 11, 18),
                    FirstName = "Alan",
                    SecondName = "Mur",
                    Email = "alan.mur@gmail.com",
                    Phone = "+38093999999"
                }
            };
        }
    }

    class PersonalInfo : PersonalInformation
    {

    }
}