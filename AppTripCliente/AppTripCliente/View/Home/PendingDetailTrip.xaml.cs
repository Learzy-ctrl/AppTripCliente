﻿using AppTripCliente.Model;
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
    public partial class PendingDetailTrip : ContentPage
    {
        public PendingDetailTrip(TripModel tripModel)
        {
            InitializeComponent();
            BindingContext = new PendingDetailTripVM(Navigation, tripModel);
        }
    }
}