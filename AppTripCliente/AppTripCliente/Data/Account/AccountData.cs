﻿using AppTripCliente.Conection;
using AppTripCliente.Model;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripCliente.Data.Account
{
    public class AccountData
    {
        public async Task<User> GetUser()
        {
            var userID = SecureStorage.GetAsync("UserID").Result;
            var User = await FirebaseConection.firebase.Child("Users").Child(userID).OnceSingleAsync<User>();
            return User;
        }

        public async Task InsertUser(User user)
        {
            await FirebaseConection.firebase.Child("Users").PostAsync(user);
        }

        public async Task<bool> DeleteAccountUser(string userId)
        {
            try
            {
                await FirebaseConection.firebase.Child("Users").Child(userId).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }


    }
}
