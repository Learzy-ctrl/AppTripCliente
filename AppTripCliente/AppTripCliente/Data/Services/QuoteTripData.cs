using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripCliente.Data.Services
{
    public class QuoteTripData
    {
        public async Task<bool> SendTripDataAsync(TripModel tripModel)
        {
            try
            {
                var UserId = await SecureStorage.GetAsync("UserID");
                var response = await FirebaseConection.firebase.Child("OutstandingTravelFees").Child(UserId).PostAsync(tripModel);
                var ObjectKey = response.Key;
                tripModel.Key = ObjectKey;
                await FirebaseConection.firebase.Child("OutstandingTravelFees").Child(UserId).Child(ObjectKey).PutAsync(tripModel);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
