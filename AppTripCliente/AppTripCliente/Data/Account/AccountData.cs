using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTripCliente.Data.Account
{
    public class AccountData
    {
        public async Task<User> GetUser(string userId)
        {
            var User = await FirebaseConection.firebase.Child("Users").Child(userId).OnceSingleAsync<User>();
            return User;
        }

        public async Task InsertUser(User user)
        {
            await FirebaseConection.firebase.Child("Users").PostAsync(user);
        }


    }
}
