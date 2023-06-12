using AppTripCliente.View.Login;
using AppTripCliente.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTripCliente.View.Account;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class EditAccountVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public EditAccountVM(INavigation navigation)
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

        public async Task GoToChangeEmail()
        {
            await Navigation.PushModalAsync(new ChangeEmailPage());
        }

        public async Task GoToChangePassword()
        {
            await Navigation.PushModalAsync(new ChangePasswordPage());
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand GoToChangeEmailCommand => new Command(async () => await GoToChangeEmail());
        public ICommand GoToChangePasswordCommand => new Command(async () => await GoToChangePassword());
        #endregion
    }
}
