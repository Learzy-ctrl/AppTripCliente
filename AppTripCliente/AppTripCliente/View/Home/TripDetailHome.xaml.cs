using AppTripCliente.Model;
using AppTripCliente.ViewModel.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripDetailHome : ContentPage
    {
        public TripDetailHome(TripModel tripModel)
        {
            InitializeComponent();
            BindingContext = new TripDetailHomeVM(Navigation, tripModel);
        }
    }
}