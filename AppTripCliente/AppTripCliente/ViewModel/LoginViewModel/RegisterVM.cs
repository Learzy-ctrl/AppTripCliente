using AppTripCliente.View.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.LoginViewModel
{
    public class RegisterVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public RegisterVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes


        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        // public ICommand ProcesoSimplecommand => new Command(ProcesoSimple);
        #endregion
    }
}
