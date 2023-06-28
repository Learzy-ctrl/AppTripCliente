using AppTripCliente.View;
using AppTripCliente.View.History;
using AppTripCliente.View.Home;
using AppTripCliente.View.Login;
using AppTripCliente.View.Services;
using AppTripCliente.ViewModel;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTripCliente
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var userID = SecureStorage.GetAsync("UserID").Result;
            if (!string.IsNullOrEmpty(userID))
            {
                MainPage = new TabbedPageContainer();
            }
            else
            {
                MainPage = new NavigationPage(new Login());
            }
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
