using Acr.UserDialogs;
using AppTripCliente.Data.Register;
using AppTripCliente.Firebase;
using AppTripCliente.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.LoginViewModel
{
    public class RegisterVM : BaseViewModel
    {
        private UserRepository userRepository = null;
        private RegisterData registerdata = null;

        #region Variables
        string _name;
        string _lastname;
        string _phonenumber;
        string _email;
        string _password;
        string _confirmpass;
        #endregion

        #region Constructor
        public RegisterVM(INavigation navigation)
        {
            Navigation = navigation;
            userRepository = new UserRepository();
            registerdata = new RegisterData();
        }
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
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set { SetValue(ref _phonenumber, value); }
        }
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
        public string ConfirmPassword
        {
            get { return _confirmpass; }
            set { SetValue(ref _confirmpass, value); }
        }
        #endregion

        #region Processes


        public async Task GoBack()
        {
            await Navigation.PopAsync();
        }

        public async Task RegisterMethod()
        {
            
            if (String.IsNullOrEmpty(Name))
            {
                await DisplayAlert("Alerta", "Escribe un Nombre", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(LastName))
            {
                await DisplayAlert("Alerta", "Escribe tus apellidos", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Email))
            {
                await DisplayAlert("Alerta", "Escribe un email", "Ok");
                return;
            }    
            if (String.IsNullOrEmpty(PhoneNumber))
            {
                await DisplayAlert("Alerta", "Escribe un numero de telefono", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Alerta", "Escribe una Contraseña", "Ok");
                return;
            }
            if (String.IsNullOrEmpty(ConfirmPassword))
            {
                await DisplayAlert("Alerta", "Confirma tu contraseña", "Ok");
                return;
            }
            if (Password == ConfirmPassword)
            {
                User newUser = new User();
                newUser.Name = Name;
                newUser.LastName = LastName;
                newUser.PhoneNumber = PhoneNumber;
                newUser.Email = Email;

                UserDialogs.Instance.ShowLoading("Cargando");
                string id = await userRepository.Register(Email, Password);
                 if (id != "")
                 {
                    newUser.IdUser = id;
                    await registerdata.InsertUser(newUser);
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Exito", "Se Registro Correctamente", "Ok");
                 }
                 else
                 {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Falla", "Error al Registrar Usuario", "Ok");
                 }
             }
             else
             {
                 await DisplayAlert("Alerta", "Tus contraseñas no coinciden", "Ok");
             }
            
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand RegisterCommand => new Command(async () => await RegisterMethod());
        // public ICommand ProcesoSimplecommand => new Command(ProcesoSimple);
        #endregion
    }
}
