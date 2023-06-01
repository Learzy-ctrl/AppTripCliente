using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class PendingTripsVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public PendingTripsVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes


        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        #endregion
    }
}
