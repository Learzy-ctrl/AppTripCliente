using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class PendingDetailTripVM : BaseViewModel
    {
        private readonly TripData data = null;
        #region Constructor
        public PendingDetailTripVM(INavigation navigation, TripModel tripModel)
        {
            Navigation = navigation;
            data = new TripData();
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

        public async Task CancelledTrip()
        {
            var confirm = await DisplayAlert("Cancelar", "¿Estas seguro de cancelar el viaje?", "Si", "No");
            if (confirm)
            {
                UserDialogs.Instance.ShowLoading("Cargando");
                await Task.Delay(500);
                Model.CancelledTravelDate = DateTime.Now.ToString("dd/MM/yyyy");
                var isValid = await data.CancelledTrips(Model);
                UserDialogs.Instance.HideLoading();
                if (isValid)
                {
                    await DisplayAlert("Exito", "Viaje cancelado", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error, intenta de nuevo", "OK");
                }
            }
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(GoBack);
        public ICommand CancelledTripCommand => new Command(async () => await CancelledTrip());
        #endregion
    }
}
