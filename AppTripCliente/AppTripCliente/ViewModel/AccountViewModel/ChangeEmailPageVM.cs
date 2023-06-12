using Acr.UserDialogs;
using AppTripCliente.Firebase;
using AppTripCliente.View.Login;
using AppTripCliente.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using AppTripCliente.Data.Account;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class ChangeEmailPageVM : BaseViewModel
    {
        private readonly AccountData data = null;
        private readonly UserRepository repository = null;
        #region Constructor
        public ChangeEmailPageVM(INavigation navigation)
        {
            Navigation = navigation;
            data = new AccountData();
            repository = new UserRepository();
        }
        #endregion

        #region Variables
        string _email;
        string _password;
        string _ispassword = "True";
        string _icon = "hide";
        #endregion

        #region Objetcs
        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public string Icon
        {
            get { return _icon; }
            set { SetValue(ref _icon, value); }
        }
        public string IsPassword
        {
            get { return _ispassword; }
            set { SetValue(ref _ispassword, value); }
        }
        #endregion

        #region Processes
        public void PasswordEnabled()
        {
            if(IsPassword == "True")
            {
                IsPassword = "False";
                Icon = "view";
            }
            else
            {
                IsPassword = "True";
                Icon = "hide";
            }
        }

        public async Task ChangeEmail()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            var User = await data.GetUser();
            if (Password == User.Password)
            {
                var IsValid = await repository.ChangeEmail(Email, User.Email, User.Password);
                UserDialogs.Instance.HideLoading();
                if (IsValid)
                {
                    await data.PutEmail(User, Email);
                    await DisplayAlert("Exito", "Se ha cambiado tu correo correctamnte", "OK");
                    Application.Current.MainPage = new TabbedPageContainer();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un error al cambiar el correo", "OK");
                }
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "La contraseña es incorrecta", "OK");
            }

        }

        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand PasswordEnabledCommand => new Command(PasswordEnabled);
        public ICommand ChangeEmailCommand => new Command(async () => await ChangeEmail());
        #endregion
    }
}
