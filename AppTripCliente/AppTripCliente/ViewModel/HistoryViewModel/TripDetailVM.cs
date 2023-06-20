using AppTripCliente.Model;
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
        

        #region Constructor
        public TripDetailVM(INavigation navigation, TripModel tripModel)
        {
            Navigation = navigation;
            Model = tripModel;
        }
        #endregion

        #region Variables
        TripModel _model = new TripModel();
        #endregion

        #region Objetcs
        public TripModel Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }
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
