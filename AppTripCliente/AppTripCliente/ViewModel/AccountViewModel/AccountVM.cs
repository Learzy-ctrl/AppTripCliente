using AppTripCliente.View.Login;
using AppTripCliente.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using AppTripCliente.View.Account;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class AccountVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public AccountVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public async Task GoToEditAccount()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(2000);
            await Navigation.PushModalAsync(new EditAccount());
            UserDialogs.Instance.HideLoading();
        }
        #endregion

        #region Commands
        public ICommand GoToEditAccountCommand => new Command(async () => await GoToEditAccount());
        #endregion
    }
}
