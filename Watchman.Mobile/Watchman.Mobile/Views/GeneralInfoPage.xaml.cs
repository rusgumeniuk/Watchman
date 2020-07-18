using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.PatientStates.HealthStates;
using Watchman.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralInfoPage : ContentPage
    {
        public GeneralInfoPage()
        {
            InitializeComponent();
            BindingContext = new GeneralInfoViewModel()
            {
                ActivityState = new CasualActivityState(),
                HealthState = new ThreateningHealthState()
            };
        }
    }
}