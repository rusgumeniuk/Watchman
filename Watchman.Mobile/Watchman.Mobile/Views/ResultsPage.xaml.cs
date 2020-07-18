using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Watchman.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage()
        {
            InitializeComponent();
            var sign1 = new DIA() { Value = 80 };
            var sign2 = new SYS() { Value = 120 };
            var sign3 = new HeartRate() { Value = 120 };
            var state = new HeartAndPressureHealthState()
            {
                MeasurementTime = DateTime.Now,
                Signs = new List<Sign<Guid, ushort>>()
                {
                    sign1,
                    sign2,
                    sign3
                }
            };
            //this.BindingContext = new HealthMeasurementsViewModel()
            //{
            //    HM = new ObservableCollection<HeartAndPressureHealthState>()
            //    {
            //        state
            //    }
            //};
        }
    }
}
