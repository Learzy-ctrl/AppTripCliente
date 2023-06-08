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
            InitializeComponent();
            if (!string.IsNullOrEmpty(SecureStorage.GetAsync("UserID").Result))
             {
                 var userID = SecureStorage.GetAsync("UserID").Result;
                 MainPage = new TabbedPageContainer(userID);
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
