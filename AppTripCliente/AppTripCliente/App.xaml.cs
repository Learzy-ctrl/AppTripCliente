using AppTripCliente.Firebase;
using AppTripCliente.View;
using AppTripCliente.View.Account;
using AppTripCliente.View.History;
using AppTripCliente.View.Home;
using AppTripCliente.View.Login;
using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente
{
    public partial class App : Application
    {
        public App()
        {

            var userID = SecureStorage.GetAsync("UserID").Result;
            InitializeComponent();
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
