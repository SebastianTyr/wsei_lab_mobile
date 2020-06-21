using AirMonitor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AirMonitor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private HomeViewModel ViewModel => BindingContext as HomeViewModel;

        public MapPage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);
        }

        private void Pin_InfoClicked(object sender, PinClickedEventArgs e)
        {
            ViewModel.InfoClickedCommand.Execute((sender as Pin).Address);
        }
    }
}