using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripCliente.Data.Home
{
    public class TripData
    {
        public async Task<ObservableCollection<TripModel>> GetAllPendingTrips()
        {
            var userId = SecureStorage.GetAsync("UserID").Result;
            var Data = await Task.Run(() => FirebaseConection.firebase.Child("OutstandingTravelFees").Child(userId)
            .AsObservable<TripModel>().AsObservableCollection());
            return Data;    
        }

        public async Task<ObservableCollection<TripModel>> GetAllTripQuote()
        {
            var userId = SecureStorage.GetAsync("UserID").Result;
            var Data = await Task.Run(() => FirebaseConection.firebase.Child("QuotesMade").Child(userId).
            AsObservable<TripModel>().AsObservableCollection());
            return Data;
        }

        public async Task<int> CountQuotes()
        {
            var userId = SecureStorage.GetAsync("UserID").Result;
            var data = await FirebaseConection.firebase.Child("QuotesMade").Child(userId).OnceAsync<TripModel>();
            return data.Count;
        }

        public async Task<int> CountPendingTripAsync()
        {
            var userId = SecureStorage.GetAsync("UserID").Result;
            var data = await FirebaseConection.firebase.Child("OutstandingTravelFees").Child(userId).OnceAsync<TripModel>();
            return data.Count;
        }

        public async Task<bool> SendConfirmTripAsync(TripModel tripModel)
        {
            try
            {
                var key = tripModel.Key;
                var userId = SecureStorage.GetAsync("UserID").Result;
                var response = await FirebaseConection.firebase.Child("ConfirmedTrips").Child(userId).PostAsync(tripModel);
                tripModel.Key = response.Key;
                await FirebaseConection.firebase.Child("ConfirmedTrips").Child(userId).Child(response.Key).PutAsync(tripModel);
                await FirebaseConection.firebase.Child("QuotesMade").Child(userId).Child(key).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendRejectionTripAsync(TripModel tripModel)
        {
            try
            {
                var key = tripModel.Key;
                var userId = SecureStorage.GetAsync("UserID").Result;
                var response = await FirebaseConection.firebase.Child("RejectedTrips").Child(userId).PostAsync(tripModel);
                tripModel.Key = response.Key;
                await FirebaseConection.firebase.Child("RejectedTrips").Child(userId).Child(response.Key).PutAsync(tripModel);
                await FirebaseConection.firebase.Child("QuotesMade").Child(userId).Child(key).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
