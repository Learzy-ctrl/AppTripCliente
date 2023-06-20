using AppTripCliente.Data.Home;
using AppTripCliente.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HomeViewModel
{
    public class PendingTripsVM : BaseViewModel
    {
        private readonly TripData pending = null;
        #region Constructor
        public PendingTripsVM(INavigation navigation)
        {
            Navigation = navigation;
            pending = new TripData();
            CountPendingTrip();
            PrintAllPendingTrip();
        }
        #endregion

        #region Variables
        ObservableCollection<TripModel> _triplist;
        string _collectionvsible;
        string _textvisible;
        #endregion

        #region Objetcs
        public ObservableCollection<TripModel> TripList
        {
            get { return _triplist; }
            set { SetValue(ref _triplist, value);
                OnPropertyChanged();
            }
        }
        public string CollectionVisible
        {
            get { return _collectionvsible; }
            set{SetValue(ref _collectionvsible, value);}
        }
        public string TextVisible
        {
            get { return _textvisible; }
            set { SetValue(ref _textvisible, value); }
        }

        #endregion

        #region Processes

        public async Task PrintAllPendingTrip()
        {

            TripList = await pending.GetAllPendingTrips();
        }

        public async void CountPendingTrip()
        {
            var count = await pending.CountPendingTripAsync();
            if(count != 0)
            {
                CollectionVisible = "True";
                TextVisible = "False";
            }
            else
            {
                CollectionVisible = "False";
                TextVisible = "True";
            }
        }
        public async Task GoBack()
        {
            await Navigation.PopModalAsync();
        }
        #endregion

        #region Commands
        public ICommand GoBackCommand => new Command(async () => await GoBack());
        #endregion
    }
}
