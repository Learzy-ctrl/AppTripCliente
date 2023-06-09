using Acr.UserDialogs;
using AppTripCliente.Data.Account;
using AppTripCliente.Model;
using AppTripCliente.View.Home;
using AppTripCliente.View.Login;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.ServicesVIewModel
{
    public class QuoteTripVM : BaseViewModel
    {
        private AccountData user = null;
        private User DataUser = null;
        #region Constructor
        public QuoteTripVM(INavigation navigation, string Option)
        {
            Navigation = navigation;
            OptionQuote = Option;
            user = new AccountData();
            DataUser = new User();
            GetDataUser();
        }
        #endregion

        #region Variables
        string _enable;
        string _startDate;
        string _endDate;
        string _numberOfDays;
        string _backgroudcolorbtn1 = "white";
        string _bordercolorbtn1 = "#455968";
        string _backgroudcolorbtn2 = "#455968";
        string _bordercolorbtn2 = "#455968";
        string _textcolorbtn1 = "black";
        string _textcolorbtn2 = "white";
        string _OptionQuote;
        string _name;
        string _phonenumber;

        #endregion

       

        #region Objetcs
        public string OptionQuote
        {
            get { return _OptionQuote; }
            set { SetValue(ref _OptionQuote, value); }
        }
        public string TextColorBtn1
        {
            get { return _textcolorbtn1; }
            set { SetValue(ref _textcolorbtn1, value); }
        }
        public string TextColorBtn2
        {
            get { return _textcolorbtn2; }
            set { SetValue(ref _textcolorbtn2, value); }
        }
        public string BackgroudColorBtn1
        {
            get { return _backgroudcolorbtn1; }
            set { SetValue(ref _backgroudcolorbtn1, value); }
        }
        public string BorderColorBtn1
        {
            get { return _bordercolorbtn1; }
            set { SetValue(ref _bordercolorbtn1, value); }
        }
        public string BackgroudColorBtn2
        {
            get { return _backgroudcolorbtn2; }
            set { SetValue(ref _backgroudcolorbtn2, value); }
        }
        public string BorderColorBtn2
        {
            get { return _bordercolorbtn2; }
            set { SetValue(ref _bordercolorbtn2, value); }
        }
        public string StartDate
        {
            get { return _startDate; }
            set { SetValue(ref _startDate, value);
                CalculateDays(_startDate, _endDate);
            }
        }
        public string EndDate
        {
            get { return _endDate; }
            set { SetValue(ref _endDate, value);
                CalculateDays(_startDate, _endDate);
            }
        }
        public string NumberOfDays
        {
            get { return _numberOfDays; }
            set { SetValue(ref _numberOfDays,value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set { SetValue(ref _phonenumber, value); }
        }
        public string Enable
        {
            get { return _enable; }
            set { SetValue(ref _enable, value); }
        }
        #endregion

        #region Processes

        public void CalculateDays(string _sstartDate, string _eendDate)
        {
            DateTime startDate;
            DateTime endDate;

            if (_sstartDate != null || _eendDate != null)
            {
                if (DateTime.TryParseExact(_sstartDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                DateTime.TryParseExact(_eendDate, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                {
                    TimeSpan duration = endDate.Subtract(startDate);
                    int numberOfDays = duration.Days;

                    string numberOfDaysString = numberOfDays.ToString();
                    NumberOfDays = numberOfDaysString;
                }
                else
                {
                    NumberOfDays = "";
                }
            }
            else
            {
                NumberOfDays = "-";
            }
        }
        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }
        public void WithAccount()
        {
            
            Name = DataUser.Name + " " + DataUser.LastName;
            PhoneNumber = DataUser.PhoneNumber;
            Enable = "False";

            BackgroudColorBtn1 = "#455968";
            BorderColorBtn1 = "#455968";
            TextColorBtn1 = "white";

            BackgroudColorBtn2 = "white";
            BorderColorBtn2 = "#455968";
            TextColorBtn2 = "black";
        }
        public void WithoutAccount()
        {
            Name = "";
            PhoneNumber = "";
            Enable = "True";

            BackgroudColorBtn1 = "white";
            BorderColorBtn1 = "#455968";
            TextColorBtn1 = "black";

            BackgroudColorBtn2 = "#455968";
            BorderColorBtn2 = "#455968";
            TextColorBtn2 = "white";
        }
        public async void GetDataUser()
        {
            var userData = await user.GetUser();
            DataUser = userData;
        }

        public async Task loading()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(5000);
            UserDialogs.Instance.HideLoading();
            await DisplayAlert("Tarea finalizada", "Se ha enviado tus datos Correctamente", "ok");
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand WithAccountCommand => new Command(WithAccount);
        public ICommand WithoutAccountCommand => new Command(WithoutAccount);

        public ICommand LoadingCommand => new Command(async () => await loading());
        #endregion
    }
}
