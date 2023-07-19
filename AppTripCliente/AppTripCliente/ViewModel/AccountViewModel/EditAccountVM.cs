using AppTripCliente.View.Login;
using AppTripCliente.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppTripCliente.View.Account;
using AppTripCliente.Data.Account;
using AppTripCliente.Model;
using Acr.UserDialogs;
using Firebase.Auth;

namespace AppTripCliente.ViewModel.AccountViewModel
{
    public class EditAccountVM : BaseViewModel
    {
        private readonly AccountData data = null;
        #region Constructor
        public EditAccountVM(INavigation navigation, Model.User model)
        {
            Navigation = navigation;
            data = new AccountData();
            user = model;
            PrintUserData();
        }
        #endregion

        #region Variables
        string _name;
        string _lastname;
        string _phonenumber;
        Model.User user;
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

        public  void PrintUserData()
        {
            Name = user.Name;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
        }

        public async Task PutUserData()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            user.Name = Name;
            user.LastName = LastName;
            user.PhoneNumber = PhoneNumber;

            var IsValid = await data.PutUser(user);
            UserDialogs.Instance.HideLoading();
            if (IsValid)
            {
                await DisplayAlert("Exito", "Se actualizaron los datos correctamente", "OK");
                Application.Current.MainPage = new TabbedPageContainer();
            }
            else
            {
                await DisplayAlert("Error", "No se actualizaron los datos", "OK");
            }
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand GoToChangeEmailCommand => new Command(async () => await GoToChangeEmail());
        public ICommand GoToChangePasswordCommand => new Command(async () => await GoToChangePassword());
        public ICommand PutUserDataCommand => new Command(async () => await PutUserData());
        #endregion
    }
}
