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
           Navigation.PushModalAsync(new MailBox());
        }

        public void GoToQuoteTrip(string option)
        {
            Navigation.PushModalAsync(new QuoteTrip(option));
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
