﻿using AppTripCliente.View;
using AppTripCliente.View.History;
using AppTripCliente.View.Login;
using AppTripCliente.View.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new NavigationPage(new Login());
             //MainPage = new TabbedPageContainer();
            MainPage = new MailBox();
            //MainPage = new QuoteTrip("");
           // MainPage = new TripDetail();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}