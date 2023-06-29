using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using AppTripCliente.View.Home;
using AppTripCliente.View.Services;
using System;
using System.Collections.ObjectModel;
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
        ObservableCollection<TripModel> _triplist;
        #endregion

        #region Objetcs
        public ObservableCollection<TripModel> TripList
        {
            get { return _triplist; }
            set { SetValue(ref _triplist, value);
                OnPropertyChanged();}
        }
        #endregion

        #region Processes

        public async Task PrintAllTripQuote()
        {
            if(TripList == null)
            {
                TripList = await data.GetAllTripQuote();
            }
        }

        public async Task GoToQuoteTrip()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            await Navigation.PushModalAsync(new QuoteTrip(""));
            UserDialogs.Instance.HideLoading();
        }

        public async Task GoToPendingTrips()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            await Navigation.PushModalAsync(new PendingTrips());
            UserDialogs.Instance.HideLoading();
        }

        public async Task GoToWithQuotesTripPage()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            await Navigation.PushModalAsync(new View.Home.QuotesTripPage());
            UserDialogs.Instance.HideLoading();
        }

        public async Task GoToTripDetail(TripModel tripModel)
        {
            await Navigation.PushModalAsync(new TripDetailHome(tripModel));
        }

        public async Task TripConfirm(TripModel Model)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            Model.QuoteDateConfirmed = DateTime.Now.ToString("dd/MM/yyyy");
            Model.SecondOption = "false";
            var IsValid = await data.SendConfirmTripAsync(Model);
            UserDialogs.Instance.HideLoading();
            if (IsValid)
            {
                await DisplayAlert("Exito", "Se ha confirmado tu viaje, en breve nos pondremos en contacto contigo", "ok");
            }
            else
            {
                await DisplayAlert("Error", "Ocurrio un error, vuelve a intentarlo", "ok");
            }
        }

        public async Task TripRejected(TripModel Model)
        {
            var Valid = await DisplayAlert("Rechazo", "¿Estas seguro?", "Si", "No");
            if (Valid)
            {
                if (Model.SecondOption != "true")
                {
                    var SecondOption = await DisplayAlert("Rechazo", "¿Quieres que te demos otra opcion?", "Si", "No");
                    if (SecondOption)
                    {
                        Model.SecondOption = "true";
                    }
                }
                else
                {
                    Model.SecondOption = "false";
                }
                UserDialogs.Instance.ShowLoading("Cargando");
                await Task.Delay(500);
                Model.QuoteDateRejected = DateTime.Now.ToString("dd/MM/yyyy");
                var IsValid = await data.SendRejectionTripAsync(Model);
                UserDialogs.Instance.HideLoading();
                if (IsValid)
                {
                    await DisplayAlert("Exito", "Viaje rechazado", "ok");
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error, vuelve a intentarlo", "ok");
                }
            }

        }
        #endregion

        #region Commands
        public ICommand GoToQuoteTripCommand => new Command(async () => await GoToQuoteTrip());
        public ICommand GoToPendingTripsCommand => new Command(async () => await GoToPendingTrips());
        public ICommand GoToWithQuotesTripPageCommand => new Command(async () => await GoToWithQuotesTripPage());
        public ICommand GoToTripDetailCommand => new Command<TripModel>(async (m) => await GoToTripDetail(m));
        public ICommand TripConfirmCommand => new Command<TripModel>(async (m) => await TripConfirm(m));
        public ICommand TripRejectedCommand => new Command<TripModel>(async (m) => await TripRejected(m));
        #endregion
    }
}
