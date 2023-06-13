﻿using Firebase.Auth;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppTripCliente.Firebase
{
    public class UserRepository
    {
        static string WebAPIkey = "AIzaSyAN9MIGaBRpm7E_8ImP6uEGpkQomJkjfhg";
        FirebaseAuthProvider authProvider;

        public UserRepository()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
        }
        public async Task<string> Register(string email, string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                var user = await authProvider.GetUserAsync(token.FirebaseToken);
                return user.LocalId;
            }
            else
            {
                return "";
            }

        }

        public async Task<string>SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            var user = await authProvider.GetUserAsync(token.FirebaseToken);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                await SecureStorage.SetAsync("UserID", user.LocalId);
                return token.FirebaseToken;
            }
            else
            {
                return "";
            }
            
        }

        public async Task<bool> DeleteUser(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            try
            {
                await authProvider.DeleteUserAsync(token.FirebaseToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<bool> ChangeEmail(string newEmail, string oldEmail, string password)
        {
            try
            {
                var token = await authProvider.SignInWithEmailAndPasswordAsync(oldEmail, password);
                await authProvider.ChangeUserEmail(token.FirebaseToken, newEmail);
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public async Task<bool> ChangePassword(string NewPassword, string Email, string OldPassword)
        {
            try
            {
                var token = await authProvider.SignInWithEmailAndPasswordAsync(Email, OldPassword);
                await authProvider.ChangeUserPassword(token.FirebaseToken, NewPassword);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

    }
}
