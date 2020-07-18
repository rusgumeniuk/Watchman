using System.Collections.ObjectModel;
using System.Windows.Input;

using Watchman.Mobile.Views;

namespace Watchman.Mobile.ViewModels
{
    class WatchmanViewModel : BaseViewModel
    {
        public ObservableCollection<PersonalInfo> Patients { get; set; }

        public ICommand StopCommand { get; set; }
        public ICommand DetailsCommand { get; set; }

    }
}
