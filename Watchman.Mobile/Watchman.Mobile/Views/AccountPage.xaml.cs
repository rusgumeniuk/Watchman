using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watchman.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {
        private BaseViewModel accountViewModel;
        public AccountPage()
        {
            InitializeComponent();
            BindingContext = accountViewModel = new AccountViewModel()
            {
                FirstName = "Ruslan",
                SecondName = "Humeniuk",
                LastName = "Victorovich",
                BirthDay = new DateTime(1999, 6, 2),
                Email = "rus@gmail.com",
                PhoneNumber = "+38099999999"
            };
        }
    }
}