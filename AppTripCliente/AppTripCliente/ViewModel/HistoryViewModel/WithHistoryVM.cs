using Acr.UserDialogs;
using AppTripCliente.View.History;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HistoryViewModel
{
    public class WithHistoryVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public WithHistoryVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public void GoToTripDetail()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            Navigation.PushModalAsync(new TripDetail(null));
            UserDialogs.Instance.HideLoading();
        }

        #endregion

        #region Commands
        public ICommand GoToTripDetailCommand => new Command(GoToTripDetail);

        #endregion
    }
}
