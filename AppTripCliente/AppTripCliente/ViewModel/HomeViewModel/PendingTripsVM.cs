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
            PrintAllPendingTrip();
        }
        #endregion

        #region Variables
        ObservableCollection<TripModel> _triplist;
        #endregion

        #region Objetcs
        public ObservableCollection<TripModel> TripList
        {
            get { return _triplist; }
            set { SetValue(ref _triplist, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Processes

        public async Task PrintAllPendingTrip()
        {

            TripList = await pending.GetAllPendingTrips();
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
