using AppTripCliente.Model;
using AppTripCliente.ViewModel.HistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripDetail : ContentPage
    {
        public TripDetail(TripModel tripModel, HistoryVM Hpage)
        {
            InitializeComponent();
            BindingContext = new TripDetailVM(Navigation, tripModel, Hpage);
        }
    }
}