using Acr.UserDialogs;
using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class TripDetailHomeVM : BaseViewModel
    {
        private readonly TripData data = null;
        #region Constructor
        public TripDetailHomeVM(INavigation navigation, TripModel tripModel)
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

        public async Task TripConfirm()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            Model.QuoteDateConfirmed = DateTime.Now.ToString("dd/MM/yyyy");
            var IsValid = await data.SendConfirmTripAsync(Model);
            UserDialogs.Instance.HideLoading();
            if (IsValid)
            {
                await DisplayAlert("Exito", "Se ha confirmado tu viaje, en breve nos pondremos en contacto contigo", "ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Ocurrio un error, vuelve a intentarlo", "ok");
            }
        }

        public async Task TripRejected()
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
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error, vuelve a intentarlo", "ok");
                }
            }
            
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(GoBack);
        public ICommand TripConfirmCommand => new Command(async () => await TripConfirm());
        public ICommand TripRejectedCommand => new Command(async () => await TripRejected());
        #endregion
    }
}
