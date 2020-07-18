using System.Collections.ObjectModel;
using System.Windows.Input;

using Watchman.BusinessLogic.Models.Users;
using Watchman.Mobile.Views;

namespace Watchman.Mobile.ViewModels
{
    class WatchmenViewModel : BaseViewModel
    {
        public ObservableCollection<PersonalInfo> Watchmen { get; set; }

        public ICommand AcceptCommand { get; set; }
        public ICommand RefuseCommand { get; set; }
        public ICommand BlockCommand { get; set; }
        public ICommand DenyCommand { get; set; }
    }
}
