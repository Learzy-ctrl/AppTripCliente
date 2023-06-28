using AppTripCliente.View.Login;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using AppTripCliente.View.Account;
using Xamarin.Essentials;
using AppTripCliente.Data.Account;
using AppTripCliente.Firebase;
using System;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class AccountVM : BaseViewModel
    {
        private AccountData Data = null;
        private UserRepository user = null;

        #region Constructor
        public AccountVM(INavigation navigation)
        {
            Data = new AccountData();
            user = new UserRepository();
            Navigation = navigation;
            PrintUserData();
        }
        #endregion

        #region Variables
        string _name;
        string _lastname;
        string _email;
        string _phonenumber;
        string _password;
        string _nameandlastname;
        string _userid;
        #endregion

        #region Objetcs
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }
        public string LastName
        {
            get { return _lastname; }
            set { SetValue(ref _lastname, value); }
        }
        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set { SetValue(ref _phonenumber, value); }
        }
        public string NameAndLastName
        {
            get { return _nameandlastname; }
            set { SetValue(ref _nameandlastname, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        public string UserID
        {
            get { return _userid; }
            set { SetValue(ref _userid, value); }
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
                await SecureStorage.SetAsync("UserID", "");
                Application.Current.MainPage = new NavigationPage(new Login());
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void PrintUserData()
        {
                var User = await Data.GetUser();
                NameAndLastName = User.Name + " " + User.LastName;
                Email = User.Email;
                PhoneNumber = User.PhoneNumber;
                Password = User.Password;
                UserID = User.IdUser;
        }

        public async Task DeleteAccount()
        {
            var IsValid = await DisplayAlert("Eliminar Cuenta", "¿Estas seguro de eliminar tu cuenta?" +
                "\nse perderan todos tus datos", "Si", "No");
            if (IsValid)
            {
                UserDialogs.Instance.ShowLoading("Eliminando cuenta");
                var ValidAccount = await user.DeleteUser(Email, Password);
                if (ValidAccount)
                {
                    var ValidData = await Data.DeleteAccountUser(UserID);
                    if (ValidData)
                    {
                        await Task.Delay(2500);
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Exito", "Se ha eliminado la cuenta", "ok");

                        await SecureStorage.SetAsync("UserID", "");
                        Application.Current.MainPage = new NavigationPage(new Login());
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "Hubo un error al eliminar Cuenta", "ok");
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "Hubo un error al eliminar Cuenta", "ok");
                }
                
            }
        }
        #endregion

        #region Commands
        public ICommand GoToEditAccountCommand => new Command(async () => await GoToEditAccount());

        public ICommand CloseSessionCommand => new Command(async () => await CloseSession());

        public ICommand DeleteAccountCommand => new Command(async () => await DeleteAccount());
        #endregion
    }
}
