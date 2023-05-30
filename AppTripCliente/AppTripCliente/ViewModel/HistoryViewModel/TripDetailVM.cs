using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HistoryViewModel
{
    public class TripDetailVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public TripDetailVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public void GoBack()
        {
            Navigation.PopModalAsync();
        }

        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(GoBack);

        #endregion
    }
}
