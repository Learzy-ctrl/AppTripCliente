using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.ServicesVIewModel
{
    public class MailBoxVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public MailBoxVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes


        public void GoBackToServices()
        {
            Navigation.PopModalAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackToServicesCommand => new Command(GoBackToServices);
        #endregion
    }
}
