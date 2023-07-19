using AppTripCliente.Conection;
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
            try
            {
                var userID = SecureStorage.GetAsync("UserID").Result;
                var User = await FirebaseConection.firebase.Child("Users").Child(userID).OnceSingleAsync<User>();
                return User;
            }catch
            {
                return null;
            }
            
        }

        public async Task InsertUser(User user)
        {

            await FirebaseConection.firebase.Child("Users").PostAsync(user);
        }
        public async Task<bool> DeleteAccountUser()
        {
            try
            {
                var userId = SecureStorage.GetAsync("UserID").Result;
                await FirebaseConection.firebase.Child("Users").Child(userId).DeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task PutEmail(User UserData, string email)
        {
            var userID = SecureStorage.GetAsync("UserID").Result;
            UserData.Email = email;
            await FirebaseConection.firebase.Child("Users").Child(userID).PutAsync(UserData);
            await SecureStorage.SetAsync("EmailUser", email);
        }

        public async Task PutPassword(User UserData, string NewPassword)
        {
            var userID = SecureStorage.GetAsync("UserID").Result;
            UserData.Password = NewPassword;
            await FirebaseConection.firebase.Child("Users").Child(userID).PutAsync(UserData);
            await SecureStorage.SetAsync("Password", NewPassword);
        }

        public async Task<bool> PutUser(User user)
        {
            try
            {
                var UserID = SecureStorage.GetAsync("UserID").Result;
                await FirebaseConection.firebase.Child("Users").Child(UserID).PutAsync(user);
                var name = user.Name + " " + user.LastName;
                await SecureStorage.SetAsync("NameUser", name);
                await SecureStorage.SetAsync("PhoneUser", user.PhoneNumber);
                return true;
            }catch(Exception)
            {
                return false;
            }
            
        }
    }
}
