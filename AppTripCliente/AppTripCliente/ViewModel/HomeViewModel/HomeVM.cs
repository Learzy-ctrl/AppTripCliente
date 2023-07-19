using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using AppTripCliente.View.Home;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class HomeVM : BaseViewModel
    {
        private readonly TripData data = null;
        #region Constructor
        public HomeVM(INavigation navigation, Home page)
        {
            Navigation = navigation;
            data = new TripData();
            Hpage = page;
            RefreshPage();
        }
        #endregion

        #region Variables
        List<TripModel> _triplist;
        bool _isrefreshing;
        Home Hpage;
        #endregion

        #region Objetcs
        public List<TripModel> TripList
        {
            get { return _triplist; }
            set { SetValue(ref _triplist, value);}
        }

        public bool isRefreshing
        {
            get { return _isrefreshing; }
            set { SetValue(ref _isrefreshing, value); }
        }
        #endregion

        #region Processes

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
            var test = await data.GetAllTripQuote();
            if(test != null)
            {
                await Navigation.PushModalAsync(new PendingTrips());
            }
            else
            {
                await DisplayAlert("Error", "Conectate a internet", "ok");
            }
            UserDialogs.Instance.HideLoading();
        }

        public async Task GoToTripDetail(TripModel tripModel)
        {
            await Navigation.PushModalAsync(new TripDetailHome(tripModel, this));
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
                await RefreshPage();
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
                    await RefreshPage();
                    await DisplayAlert("Exito", "Viaje rechazado", "ok");
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error, vuelve a intentarlo", "ok");
                }
            }

        }

        public async Task RefreshPage()
        {
            isRefreshing = true;
            await Task.Delay(500);
            var list = await data.GetAllTripQuote();
            if(list != null)
            {
                if (list.Count != 0)
                {
                    TripList = list;
                    Hpage.ChangePage(true, true);
                }
                else
                {
                    Hpage.ChangePage(false, true);
                }
            }
            else
            {
                Hpage.ChangePage(false, false);
            }
            isRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand GoToQuoteTripCommand => new Command(async () => await GoToQuoteTrip());
        public ICommand GoToPendingTripsCommand => new Command(async () => await GoToPendingTrips());
        public ICommand GoToTripDetailCommand => new Command<TripModel>(async (m) => await GoToTripDetail(m));
        public ICommand TripConfirmCommand => new Command<TripModel>(async (m) => await TripConfirm(m));
        public ICommand TripRejectedCommand => new Command<TripModel>(async (m) => await TripRejected(m));
        public ICommand RefreshPageCommand => new Command(async () => await RefreshPage());
        #endregion
    }
}
