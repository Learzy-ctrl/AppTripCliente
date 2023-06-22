using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using AppTripCliente.View.Home;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class PendingTripsVM : BaseViewModel
    {
        private readonly TripData pending = null;
        #region Constructor
        public PendingTripsVM(INavigation navigation)
        {
            Navigation = navigation;
            pending = new TripData();
            CountPendingTrip();
            PrintAllPendingTrip();
        }
        #endregion

        #region Variables
        ObservableCollection<TripModel> _triplist;
        string _collectionvsible;
        string _textvisible;
        #endregion

        #region Objetcs
        public ObservableCollection<TripModel> TripList
        {
            get { return _triplist; }
            set { SetValue(ref _triplist, value);
                OnPropertyChanged();
            }
        }
        public string CollectionVisible
        {
            get { return _collectionvsible; }
            set{SetValue(ref _collectionvsible, value);}
        }
        public string TextVisible
        {
            get { return _textvisible; }
            set { SetValue(ref _textvisible, value); }
        }

        #endregion

        #region Processes

        public async Task PrintAllPendingTrip()
        {

            TripList = await pending.GetAllPendingTrips();
        }

        public async void CountPendingTrip()
        {
            var count = await pending.CountPendingTripAsync();
            if(count != 0)
            {
                CollectionVisible = "True";
                TextVisible = "False";
            }
            else
            {
                CollectionVisible = "False";
                TextVisible = "True";
            }
        }
        public async Task GoToTripDetail(TripModel tripModel)
        {
            await Navigation.PushModalAsync(new PendingDetailTrip(tripModel));
        }
        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }

        public async Task CancelledTrip(TripModel tripModel)
        {
            var confirm = await DisplayAlert("Cancelar", "¿Estas seguro de cancelar el viaje?", "Si", "No");
            if (confirm)
            {
                UserDialogs.Instance.ShowLoading("Cargando");
                await Task.Delay(500);
                var isValid = await pending.CancelledTrips(tripModel);
                UserDialogs.Instance.HideLoading();
                if (isValid)
                {
                    await DisplayAlert("Exito", "Viaje cancelado", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error, intenta de nuevo", "OK");
                }
            }
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand GoToTripDetailCommand => new Command<TripModel>(async (m) => await GoToTripDetail(m));
        public ICommand CancelledTripCommand => new Command<TripModel>(async (m) => await CancelledTrip(m));
        #endregion
    }
}
