using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripCliente.Data.History
{
    public class HistoryData
    {
        public async Task<List<TripModel>> GetAllHistory()
        {
            try
            {
                var listTrip = new List<TripModel>();
                var id = SecureStorage.GetAsync("UserID").Result;
                var list = await FirebaseConection.firebase.Child("HistoryTrips").Child(id).OnceAsync<TripModel>();
                foreach (var l in list)
                {
                    listTrip.Add(l.Object);
                }
                return listTrip;
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<bool> DeleteHistoryAsync(string key)
        {
            try
            {
                var id = SecureStorage.GetAsync("UserID").Result;
                await FirebaseConection.firebase.Child("HistoryTrips").Child(id).Child(key).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
