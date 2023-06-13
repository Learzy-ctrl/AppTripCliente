using Acr.UserDialogs;
using AppTripCliente.Data.Register;
using AppTripCliente.Firebase;
using AppTripCliente.Model;
using AppTripCliente.View;
using System;
using System.IO;
using System.Linq;
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
        string _ispassword1 = "True";
        string _icon1 = "hide";
        string _ispassword2 = "True";
        string _icon2 = "hide";
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

        public string IsPassword1
        {
            get { return _ispassword1; }
            set { SetValue(ref _ispassword1, value); }
        }

        public string Icon1
        {
            get { return _icon1; }
            set { SetValue(ref _icon1, value); }
        }

        public string IsPassword2
        {
            get { return _ispassword2; }
            set { SetValue(ref _ispassword2, value); }
        }
        public string Icon2
        {
            get { return _icon2; }
            set { SetValue(ref _icon2, value); }
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
            if (Password.Count() < 6)
            {
                await DisplayAlert("Alerta", "La contraseña debe ser mayor a 6 caracteres", "Ok");
                return;
            }
            if (Password == ConfirmPassword)
            {
                User newUser = new User();
                newUser.Name = Name;
                newUser.LastName = LastName;
                newUser.PhoneNumber = PhoneNumber;
                newUser.Email = Email;
                newUser.Password = Password;

                UserDialogs.Instance.ShowLoading("Cargando");
                try
                {
                    string id = await userRepository.Register(Email, Password);
                    if (id != "")
                    {
                        newUser.IdUser = id;
                        await registerdata.InsertUser(newUser);

                        await userRepository.SignIn(Email, Password);
                        await Navigation.PushAsync(new PageEmpty());
                        await Task.Delay(2000);
                        Application.Current.MainPage = new TabbedPageContainer();
                        await Task.Delay(1400);
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Falla", "Error al Registrar Usuario", "Ok");
                    }
                }catch(Exception e)
                {
                    if (e.Message.Contains("INVALID_EMAIL"))
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "Correo electronico invalido", "ok");
                    }
                    if (e.Message.Contains("EMAIL_EXISTS"))
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Error", "El correo ya esta registrado", "ok");
                    }
                }
                
             }
             else
             {
                 await DisplayAlert("Alerta", "Las contraseñas no coinciden", "Ok");
             }
            
        }

        public void IsEnabledPassword()
        {
            if(IsPassword1 == "True")
            {
                IsPassword1 = "False";
                Icon1 = "view";
            }
            else
            {
                IsPassword1 = "True";
                Icon1 = "hide";
            }
        }
        public void IsEnabledConfirmPassword()
        {
            if (IsPassword2 == "True")
            {
                IsPassword2 = "False";
                Icon2 = "view";
            }
            else
            {
                IsPassword2 = "True";
                Icon2 = "hide";
            }
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand RegisterCommand => new Command(async () => await RegisterMethod());
        public ICommand IsEnabledPasswordCommand => new Command(IsEnabledPassword);
        public ICommand IsEnabledConfirmPasswordCommand => new Command(IsEnabledConfirmPassword);
        #endregion
    }
}
