using Acr.UserDialogs;
using AppTripCliente.Data.Account;
using AppTripCliente.Firebase;
using AppTripCliente.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class ChangePasswordVM : BaseViewModel
    {
        private readonly AccountData data = null;
        private readonly UserRepository repository = null;
        #region Constructor
        public ChangePasswordVM(INavigation navigation)
        {
            Navigation = navigation;
            data = new AccountData();
            repository = new UserRepository();
        }
        #endregion

        #region Variables
        string _oldpassword;
        string _newpassword;
        string _confirmpassword;
        string _ispassword1 = "True";
        string _icon1 = "hide";
        string _ispassword2 = "True";
        string _icon2 = "hide";
        string _ispassword3 = "True";
        string _icon3 = "hide";
        #endregion

        #region Objetcs
        public string OldPassword
        {
            get { return _oldpassword; }
            set { SetValue(ref _oldpassword, value); }
        }
        public string NewPassword
        {
            get { return _newpassword; }
            set { SetValue(ref _newpassword, value); }
        }
        public string ConfirmPassword
        {
            get { return _confirmpassword; }
            set { SetValue(ref _confirmpassword, value); }
        }
        public string Icon1
        {
            get { return _icon1; }
            set { SetValue(ref _icon1, value); }
        }
        public string IsPassword1
        {
            get { return _ispassword1; }
            set { SetValue(ref _ispassword1, value); }
        }
        public string Icon2
        {
            get { return _icon2; }
            set { SetValue(ref _icon2, value); }
        }
        public string IsPassword2
        {
            get { return _ispassword2; }
            set { SetValue(ref _ispassword2, value); }
        }
        public string Icon3
        {
            get { return _icon3; }
            set { SetValue(ref _icon3, value); }
        }
        public string IsPassword3
        {
            get { return _ispassword3; }
            set { SetValue(ref _ispassword3, value); }
        }
        #endregion

        #region Processes
        public void PasswordEnabled1()
        {
            if (IsPassword1 == "True")
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
        public void PasswordEnabled2()
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
        public void PasswordEnabled3()
        {
            if (IsPassword3 == "True")
            {
                IsPassword3 = "False";
                Icon3 = "view";
            }
            else
            {
                IsPassword3 = "True";
                Icon3 = "hide";
            }
        }

        public async Task ChangePassword()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            var user = await data.GetUser();
            if(OldPassword == user.Password)
            {
                if(NewPassword.Count() >= 6)
                {
                    if (NewPassword == ConfirmPassword)
                    {
                        var IsValid = await repository.ChangePassword(NewPassword, user.Email, OldPassword);
                        if (IsValid)
                        {
                            await data.PutPassword(user, NewPassword);
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Exito", "La contraseña se actualizo correctamente", "OK");
                            await Navigation.PopModalAsync();
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Error", "Ocurrio un error al actualizar la contraseña", "OK");
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await DisplayAlert("Alerta", "La contraseña no coincide con la confirmacion", "OK");
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Error", "La contraseña debe tener minimo 6 Caracteres", "OK");
                }
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Error", "Contraseña Incorrecta", "OK");
            }
        }

        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand PasswordEnabled1Command => new Command(PasswordEnabled1);
        public ICommand PasswordEnabled2Command => new Command(PasswordEnabled2);
        public ICommand PasswordEnabled3Command => new Command(PasswordEnabled3);
        public ICommand ChangePasswordCommand => new Command(async () => await ChangePassword());
        #endregion
    }
}
