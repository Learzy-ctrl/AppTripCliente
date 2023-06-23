using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using AppTripCliente.View.Home;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class QuotesTripPageVM : BaseViewModel
    {
        private readonly TripData data = null;
        #region Constructor
        public QuotesTripPageVM(INavigation navigation)
        {
            Navigation = navigation;
            data = new TripData();
            QuoteExist();
        }
        #endregion

        #region Variables
        ObservableCollection<TripModel> _triplist = new ObservableCollection<TripModel>();
        string _isnull;
        string _visible = "True";
        #endregion

        #region Objetcs
        public ObservableCollection<TripModel> TripList
        {
            get { return _triplist; }
            set
            {
                SetValue(ref _triplist, value);
                OnPropertyChanged();
            }
        }

        public string IsNull
        {
            get { return _isnull; }
            set { SetValue(ref _isnull, value); }
        }
        public string Visible
        {
            get { return _visible; }
            set { SetValue(ref _visible, value); }
        }
        #endregion

        #region Processes
        public async Task QuoteExist()
        {
            var count = await data.CountQuotes();
            if(count != 0)
            {
                IsNull = "True";
                Visible = "False";
                await PrintAllTripQuote();
            }
            else
            {
                IsNull = "False";
                Visible = "True";
            }
        }
        public async Task PrintAllTripQuote()
        {
            TripList = await data.GetAllTripQuote();
        }

        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
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
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand GoToTripDetailCommand => new Command<TripModel>(async (m) => await GoToTripDetail(m));
        public ICommand TripConfirmCommand => new Command<TripModel>(async (m) => await TripConfirm(m));
        public ICommand TripRejectedCommand => new Command<TripModel>(async (m) => await TripRejected(m));
        #endregion
    }
}
