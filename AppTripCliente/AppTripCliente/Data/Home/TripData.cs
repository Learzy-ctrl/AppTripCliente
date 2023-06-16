using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
    }
}
