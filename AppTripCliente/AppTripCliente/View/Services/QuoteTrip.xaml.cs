﻿using AppTripCliente.ViewModel.ServicesVIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuoteTrip : ContentPage
	{
		public QuoteTrip (string option)
		{
			InitializeComponent ();
			BindingContext = new QuoteTripVM(Navigation, option);
		}
	}
}