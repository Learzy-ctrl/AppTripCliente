using AppTripCliente.View.Login;
using AppTripCliente.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        // public ICommand ProcesoSimplecommand => new Command(ProcesoSimple);
        #endregion
    }
}
