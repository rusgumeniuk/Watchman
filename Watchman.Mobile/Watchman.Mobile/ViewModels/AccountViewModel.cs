using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Watchman.Mobile.ViewModels
{
    class AccountViewModel : BaseViewModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }

        public ICommand SubmitCommand { get; set; }

        public AccountViewModel()
        {
            SubmitCommand = new Command(Submit);
        }

        private void Submit()
        {
            
        }
    }
}
