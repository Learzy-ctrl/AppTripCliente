using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HistoryViewModel
{
    public class WithoutHistoryVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public WithoutHistoryVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public void GoToQuoteTrip()
        {
            Navigation.PushModalAsync(new QuoteTrip(""));
        }

        #endregion

        #region Commands
        public ICommand GoToQuoteTripCommand => new Command(GoToQuoteTrip);

        #endregion
    }
}
