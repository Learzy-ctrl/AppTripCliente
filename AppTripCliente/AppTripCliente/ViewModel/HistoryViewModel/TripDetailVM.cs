using Acr.UserDialogs;
using AppTripCliente.Data.History;
using AppTripCliente.Model;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HistoryViewModel
{
    public class TripDetailVM : BaseViewModel
    {
        private readonly HistoryData data = null;
        #region Constructor
        public TripDetailVM(INavigation navigation, TripModel tripModel, HistoryVM Hpage)
        {
            Navigation = navigation;
            Model = tripModel;
            page = Hpage;
            data = new HistoryData();
        }
        #endregion

        #region Variables
        TripModel _model = new TripModel();
        HistoryVM page;
        #endregion

        #region Objetcs
        public TripModel Model
        {
            get { return _model; }
            set { SetValue(ref _model, value); }
        }
        #endregion

        #region Processes

        public void GoBack()
        {
            Navigation.PopModalAsync();
        }

        public async Task DeleteTripHistory()
        {
            var isBool = await DisplayAlert("Eliminar viaje", "¿Estas seguro de eliminar el viaje?", "Si", "No");
            if (isBool)
            {
                UserDialogs.Instance.ShowLoading("Eliminando");
                await Task.Delay(500);
                var IsValid = await data.DeleteHistoryAsync(Model.Key);
                UserDialogs.Instance.HideLoading();
                if (IsValid)
                {
                    await page.RefreshingView();
                    await DisplayAlert("Exito", "Se ha eliminado correctamente", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Se ha interrumpido la accion, intenta de nuevo", "OK");
                }
            }
        }

        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(GoBack);
        public ICommand DeleteTripHistoryCommand => new Command(async () => await DeleteTripHistory());

        #endregion
    }
}
