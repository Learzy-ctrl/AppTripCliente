using Acr.UserDialogs;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.ServicesVIewModel
{
    public class ServicesVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public ServicesVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes


        public void GoToMailBox()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            Navigation.PushModalAsync(new MailBox());
            UserDialogs.Instance.HideLoading();
        }

        public void GoToQuoteTrip(string option)
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            Navigation.PushModalAsync(new QuoteTrip(option));
            UserDialogs.Instance.HideLoading();
        }
        #endregion

        #region Commands
        public ICommand GoToMailBoxCommand => new Command(GoToMailBox);
        public ICommand GoToQuoteTrip1Command => new Command(() => GoToQuoteTrip("Transporte Ejecutivo"));
        public ICommand GoToQuoteTrip2Command => new Command(() => GoToQuoteTrip("Transporte de personal"));
        public ICommand GoToQuoteTrip3Command => new Command(() => GoToQuoteTrip("Servicios Especiales"));
        #endregion
    }
}
