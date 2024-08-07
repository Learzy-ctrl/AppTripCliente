﻿using Acr.UserDialogs;
using AppTripCliente.Data.Account;
using AppTripCliente.Data.Services;
using AppTripCliente.Model;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.ServicesVIewModel
{
    public class QuoteTripVM : BaseViewModel
    {
        private AccountData user = null;
        private QuoteTripData tripData = null;
        #region Constructor
        public QuoteTripVM(INavigation navigation, string Option)
        {
            Navigation = navigation;
            OptionQuote = Option;
            user = new AccountData();
            tripData = new QuoteTripData();;
        }
        #endregion

        #region Variables
        string _enable;
        string _enablepicker;
        string _backgroudcolorbtn1 = "white";
        string _bordercolorbtn1 = "#455968";
        string _backgroudcolorbtn2 = "#455968";
        string _bordercolorbtn2 = "#455968";
        string _textcolorbtn1 = "black";
        string _textcolorbtn2 = "white";
        string _ischeckedyes;
        string _ischeckedno;
        string _OptionQuote;
        string _pointorigin;
        string _endpoint;
        string _name;
        string _phonenumber;
        string _feedback;
        DateTime _startDate = DateTime.Now;
        DateTime _endDate = DateTime.Now;
        TimeSpan _startdatetime;
        TimeSpan _backdatetime;
        string _rounded;
        string _numberpassengers;
        #endregion

        #region Objetcs
        public string EnablePicker
        {
            get { return _enablepicker; }
            set { SetValue(ref _enablepicker, value); }
        }
        public string IsCheckedYes
        {
            get { return _ischeckedyes; }
            set { SetValue(ref _ischeckedyes, value); }
        }
        public string IsCheckedNo
        {
            get { return _ischeckedno; }
            set { SetValue(ref _ischeckedno, value); DatePickerEnabled(); }
        }
        public string Enable
        {
            get { return _enable; }
            set { SetValue(ref _enable, value); }
        }
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
        public string PointOrigin
        {
            get { return _pointorigin; }
            set { SetValue(ref _pointorigin, value); }
        }
        public string EndPoint
        {
            get { return _endpoint; }
            set { SetValue(ref _endpoint, value); }
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
        public string FeedBack
        {
            get { return _feedback; }
            set { SetValue(ref _feedback, value); }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetValue(ref _startDate, value); }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetValue(ref _endDate, value);}
        }
        public TimeSpan StartDateTime
        {
            get { return _startdatetime; }
            set { SetValue(ref _startdatetime, value); }
        }
        public TimeSpan BackDateTime
        {
            get { return _backdatetime; }
            set { SetValue(ref _backdatetime, value); }
        }
        public string Rounded
        {
            get { return _rounded; }
            set { SetValue(ref _rounded, value); }
        }
        public string NumberPassengers
        {
            get { return _numberpassengers; }
            set { SetValue(ref _numberpassengers, value); }
        }
        #endregion

        #region Processes
        public void DatePickerEnabled()
        {
            if(IsCheckedNo == "True")
            {
                EnablePicker = "False";
            }
            else
            {
                EnablePicker = "True";
            }
        }
        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }
        public void WithAccount()
        {
            var name = SecureStorage.GetAsync("NameUser").Result;
            var phone = SecureStorage.GetAsync("PhoneUser").Result;
            Name = name;
            PhoneNumber = phone;
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
        public async Task QuestionDateTimeStart()
        {
            await DisplayAlert("Hora Inicio", "La hora seleccionada indicara la hora que iniciara el viaje desde el punto origen", "ok");
        }
        public async Task QuestionDateTimeBack()
        {
            await DisplayAlert("Hora Regreso", "La hora seleccionada indicara la hora que iniciara el viaje de retorno", "ok");
        }
        public async Task SendTripData()
        {
            
            if (string.IsNullOrEmpty(PointOrigin))
            {
                await DisplayAlert("Alerta", "Indica donde empezaremos", "OK");
                return;
            }
            if (string.IsNullOrEmpty(EndPoint))
            {
                await DisplayAlert("Alerta", "Indica a donde vamos", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                await DisplayAlert("Alerta", "Ingresa tu nombre", "OK");
                return;
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await DisplayAlert("Alerta", "Ingresa tu numero celular", "OK");
                return;
            }
            if (string.IsNullOrEmpty(NumberPassengers))
            {
                await DisplayAlert("Alerta", "Indica el numero de pasajeros", "OK");
                return;
            }
            if (string.IsNullOrEmpty(IsCheckedYes) && string.IsNullOrEmpty(IsCheckedNo))
            {
                await DisplayAlert("Viaje retorno", "Selecciona una casilla", "OK");
                return;
            }
            if (IsCheckedYes != IsCheckedNo)
            {
                UserDialogs.Instance.ShowLoading("Cargando");
                TripModel tripModel = new TripModel();
                if(IsCheckedYes == "True" && IsCheckedNo == "False" || IsCheckedNo == null)
                {
                    Rounded = "Si";
                }
                else
                {
                    Rounded = "No";
                }
                tripModel.PointOrigin = PointOrigin;
                tripModel.EndPoint = EndPoint;
                tripModel.Name = Name;
                tripModel.PhoneNumber = PhoneNumber;
                tripModel.FeedBack = FeedBack;
                tripModel.StartDate = StartDate.ToString("dd/MM/yyyy");
                tripModel.StartDateTime = StartDateTime.Hours.ToString() + ":" + TimeMinutes(StartDateTime);
                if(EnablePicker == "True" || EnablePicker == null)
                {
                    tripModel.BackDateTime = BackDateTime.Hours.ToString() + ":" + TimeMinutes(BackDateTime);
                    tripModel.EndDate = EndDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    tripModel.BackDateTime = "Sin Retorno";
                    tripModel.EndDate = "Sin Retorno";
                }
                tripModel.Rounded = Rounded;
                tripModel.NumberPassengers = NumberPassengers;
                tripModel.OptionQuote = OptionQuote;
                tripModel.QuoteDateSent = DateTime.Now.ToString("dd/MM/yyyy");
                tripModel.IdDevice = await SecureStorage.GetAsync("IdDevice");
                tripModel.ConfirmedChecked = "false";
                var IsValid = await tripData.SendTripDataAsync(tripModel);
                UserDialogs.Instance.HideLoading();
                if (IsValid)
                {
                    await DisplayAlert("Exito", "Se ha enviado tu informacion correctamente, en unos momentos te enviaremos una respuesta", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio algo inesperado, vuelve a intentar", "OK");
                }
            }
            else
            {
                await DisplayAlert("Viaje retorno", "Selecciona solo una casilla", "OK");
            }
        }

        public string TimeMinutes(TimeSpan Minutes)
        {
            if(Minutes.Minutes < 10)
            {
                var strMinutes = "0" + Minutes.Minutes.ToString();
                return strMinutes;
            }
            else
            {
                return Minutes.Minutes.ToString();
            }
        }

        public string ReformatDate(string date)
        {
            string dateStr = date;
            DateTime newdate;
            if (DateTime.TryParseExact(dateStr, "dd/MM/yyyy HHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out newdate))
            {
                string reformatdate = newdate.ToString("dd/MM/yyyy");
                return reformatdate;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        public ICommand WithAccountCommand => new Command(WithAccount);
        public ICommand WithoutAccountCommand => new Command(WithoutAccount);
        public ICommand SendTripDataCommand => new Command(async () => await SendTripData());
        public ICommand QuestionDateTimeStartCommand => new Command(async () => await QuestionDateTimeStart());
        public ICommand QuestionDateTimeBackCommand => new Command(async () => await QuestionDateTimeBack());
        #endregion
    }
}
