using Acr.UserDialogs;
using AppTripCliente.Firebase;
using AppTripCliente.View;
using AppTripCliente.View.Home;
using AppTripCliente.View.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.LoginViewModel
{
    public class LoginVM : BaseViewModel
    {
        private UserRepository repository = null;
        #region Constructor
        public LoginVM(INavigation navigation)
        {
            Navigation = navigation;
            repository = new UserRepository();
        }
        #endregion

        #region Variables
        string _email;
        string _password;
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
        #endregion

        #region Processes


        public async Task GoToRegister()
        {
            await Navigation.PushAsync(new Register());
        }
        public async Task GoToTabbedpage()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Cargando");
                var token = await repository.SignIn(Email, Password);

                if (!string.IsNullOrEmpty(token))
                {
                    var userID = SecureStorage.GetAsync("UserID").Result;
                    await Navigation.PopAsync();
                    Application.Current.MainPage = new TabbedPageContainer(userID);
                    await Task.Delay(1400);
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Acceso denegado", "El correo o contrasea son invalidos, intente denuevo", "ok");
                }
            }catch(Exception e)
            {
                if (e.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Acceso denegado", "El correo no esta registrado", "ok");
                }
                else if (e.Message.Contains("INVALID_PASSWORD"))
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Acceso denegado", "La contraseña es invalida", "ok");
                }
                else if(e.Message.Contains("INVALID_EMAIL"))
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Correo electronico invalido", "ok");
                }
            }
           
            
        }

        #endregion

        #region Commands
        public ICommand GoToRegisterCommand => new Command(async () => await GoToRegister());
        public ICommand GoToTabbedpageCommand => new Command(async () => await GoToTabbedpage());
        // public ICommand ProcesoSimplecommand => new Command(ProcesoSimple);
        #endregion
    }
}
