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
            await Navigation.PushModalAsync(new QuoteTrip(""));
        }

        public async Task GoToPendingTrips()
        {
            await Navigation.PushModalAsync(new PendingTrips());
        }
        #endregion

        #region Commands
        public ICommand GoToQuoteTripCommand => new Command(async () => await GoToQuoteTrip());
        public ICommand GoToPendingTripsCommand => new Command(async () => await GoToPendingTrips());
        #endregion
    }
}
