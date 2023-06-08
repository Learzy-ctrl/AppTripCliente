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
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class AccountVM : BaseViewModel
    {
        #region Variables
        string _userId;
        #endregion

        #region Constructor
        public AccountVM(INavigation navigation, string id)
        {
            Navigation = navigation;
            _userId = id;
        }
        #endregion

        #region Objetcs
        public string Name
        {
            get { return _userId; }
            set { SetValue(ref _userId, value); }
        }
        #endregion

        #region Processes

        public async Task GoToEditAccount()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(2000);
            await Navigation.PushModalAsync(new EditAccount());
            UserDialogs.Instance.HideLoading();
        }

        public async Task CloseSession()
        {
            
            var IsValid = await DisplayAlert("Cerrar Sesion", "¿Estas seguro de cerrar sesion?", "Si", "No");
            
            if (IsValid)
            {
                UserDialogs.Instance.ShowLoading("Cerrando Sesion");
                Page currentPage = Application.Current.MainPage;

                // Envuelve la página actual con un NavigationPage
                NavigationPage navigationPage = new NavigationPage(currentPage);
                Application.Current.MainPage = navigationPage;
                await navigationPage.Navigation.PopAsync();

                await SecureStorage.SetAsync("UserID", "");
                Application.Current.MainPage = new Login();
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion

        #region Commands
        public ICommand GoToEditAccountCommand => new Command(async () => await GoToEditAccount());

        public ICommand CloseSessionCommand => new Command(async () => await CloseSession());
        #endregion
    }
}
