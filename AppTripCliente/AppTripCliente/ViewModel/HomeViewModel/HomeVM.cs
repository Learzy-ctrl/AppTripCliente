using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using AppTripCliente.View.Home;
using AppTripCliente.View.Login;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class HomeVM : BaseViewModel
    {
        private readonly TripData data = null;
        #region Constructor
        public HomeVM(INavigation navigation)
        {
            Navigation = navigation;
            data = new TripData();
            PrintAllTripQuote();
        }
        #endregion

        #region Variables
        ObservableCollection<TripModel> _triplist = new ObservableCollection<TripModel>();
        string _isvisible;
        #endregion

        #region Objetcs
        public ObservableCollection<TripModel> TripList
        {
            get { return _triplist; }
            set { SetProperty(ref _triplist, value);
                  //OnPropertyChanged();
                  CardEnabled();
            }
        }

        public string VisibleValidation
        {
            get { return _isvisible; }
            set { SetProperty(ref _isvisible, value); }
        }
        #endregion

        #region Processes

        public void CardEnabled()
        {
            if(TripList.Count > 0)
            {
                VisibleValidation = "True";
            }
            else
            {
                VisibleValidation = "False";
            }
        }
        public async Task PrintAllTripQuote()
        {
            TripList = await data.GetAllTripQuote();
        }

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
