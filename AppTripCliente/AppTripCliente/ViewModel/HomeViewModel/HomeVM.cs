using Acr.UserDialogs;
using AppTripCliente.View.Home;
using AppTripCliente.View.Login;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class HomeVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public HomeVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes


        public async Task GoToQuoteTrip()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Navigation.PushModalAsync(new QuoteTrip(""));
            UserDialogs.Instance.HideLoading();
        }

        public async Task GoToPendingTrips()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Navigation.PushModalAsync(new PendingTrips());
            UserDialogs.Instance.HideLoading();
        }
        #endregion

        #region Commands
        public ICommand GoToQuoteTripCommand => new Command(async () => await GoToQuoteTrip());
        public ICommand GoToPendingTripsCommand => new Command(async () => await GoToPendingTrips());
        #endregion
    }
}
