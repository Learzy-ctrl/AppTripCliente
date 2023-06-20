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
    public partial class QuotesTripPage : ContentPage
    {
        public QuotesTripPage()
        {
            InitializeComponent();
            BindingContext = new QuotesTripPageVM(Navigation);
        }
    }
}