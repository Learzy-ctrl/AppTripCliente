using Acr.UserDialogs;
using AppTripCliente.Data.History;
using AppTripCliente.Model;
using AppTripCliente.View.History;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HistoryViewModel
{
    public class HistoryVM : BaseViewModel
    {
        private readonly HistoryData data = null;
        #region Constructor
        public HistoryVM(INavigation navigation, History page)
        {
            Navigation = navigation;
            historypage = page;
            data = new HistoryData(); 
            RefreshingView();
        }
        #endregion

        #region Variables
        History historypage;
        string _isrefreshing = "False";
        bool flag = false;
        List<TripModel> _list;
        #endregion

        #region Objetcs
        public string isRefreshing
        {
            get { return _isrefreshing; }
            set { SetValue(ref _isrefreshing, value); }
        }
        public List<TripModel> HistoryList
        {
            get { return _list; }
            set { SetValue(ref _list, value); }
        }
        #endregion

        #region Processes


        public async Task RefreshingView()
        {
            isRefreshing = "True";
            var list = await data.GetAllHistory();
            if(list != null)
            {
                if (list.Count != 0)
                {
                    HistoryList = list;
                    historypage.ChangeView(true);
                }
                else
                {
                    historypage.ChangeView(false);
                }
            }
            isRefreshing = "False";
        }

        public async Task GoToQuoteTrip()
        {
            UserDialogs.Instance.ShowLoading("Cargando");
            await Task.Delay(500);
            await Navigation.PushModalAsync(new QuoteTrip(""));
            UserDialogs.Instance.HideLoading();
        }
        public async Task GoToTripDetail(TripModel model)
        {
            await Navigation.PushModalAsync(new TripDetail(model, this));
        }

        #endregion

        #region Commands
        public ICommand GoToQuoteTripCommand => new Command(async () => await GoToQuoteTrip());
        public ICommand GoToTripDetailCommand => new Command<TripModel>(async (m) => await GoToTripDetail(m));
        public ICommand RefreshingViewCommand => new Command(async () => await RefreshingView());

        #endregion
    }
}
